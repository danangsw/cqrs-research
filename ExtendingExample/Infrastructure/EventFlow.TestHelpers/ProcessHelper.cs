using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.TestHelpers.Extensions;

namespace EventFlow.TestHelpers
{
    public static class ProcessHelper
    {
        public static IDisposable StartExe(
            string exePath,
            string initializationDone,
            params string[] arguments)
        {
            if (string.IsNullOrEmpty(exePath)) throw new ArgumentNullException(nameof(exePath));
            if (string.IsNullOrEmpty(initializationDone)) throw new ArgumentNullException(nameof(initializationDone));

            var workingDirectory = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(workingDirectory)) throw new ArgumentException($"Could not find directory for '{exePath}'", nameof(exePath));

            LogHelper.Log.Information($"Starting process: '{exePath}' {string.Join(" ", arguments)}");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(exePath, string.Join(" ", arguments))
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = workingDirectory,
                }
            };
            var exeName = Path.GetFileName(exePath);
            DataReceivedEventHandler outHandler = (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    LogHelper.Log.Information($"OUT - {exeName}: {e.Data}");
                }
            };
            process.OutputDataReceived += outHandler;
            DataReceivedEventHandler errHandler = (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    LogHelper.Log.Error($"ERR - {exeName}: {e.Data}");
                }
            };
            process.ErrorDataReceived += errHandler;
            Action<Process> initializeProcess = p =>
            {
                LogHelper.Log.Information($"{exeName} START =======================================");
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            };
            process.WaitForOutput(initializationDone, initializeProcess);

            return new DisposableAction(() =>
            {
                try
                {
                    process.OutputDataReceived -= outHandler;
                    process.ErrorDataReceived -= errHandler;

                    KillProcessAndChildren(process.Id);
                }
                catch (Exception e)
                {
                    LogHelper.Log.Error($"Failed to kill process: {e.Message}");
                }
                finally
                {
                    process.DisposeSafe("Process");
                }
            });
        }

        private static void KillProcessAndChildren(int pid)
        {
            var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            var moc = searcher.Get();

            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }

            try
            {
                LogHelper.Log.Information($"Killing process {pid}");

                var proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
    }
}
