// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Commons
{
    public class ResponseResult
    {
        private static readonly ResponseResult _success = new ResponseResult { Succeeded = true };
        private List<ResponseError> _errors = new List<ResponseError>();

        private List<object> _responses = new List<object>();

        /// <summary>
        /// Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// An <see cref="IEnumerable{T}"/> of <see cref="ResponseError"/>s containing an errors
        /// that occurred during the response operation.
        /// </summary>
        /// <value>An <see cref="IEnumerable{T}"/> of <see cref="ResponseError"/>s.</value>
        public IEnumerable<ResponseError> Errors => _errors;

        public IEnumerable<object> Responses => _responses;

        /// <summary>
        /// Creates an <see cref="ResponseResult"/> indicating a failed response operation, with a list of <paramref name="errors"/> if applicable.
        /// </summary>
        /// <param name="errors">An optional array of <see cref="ResponseError"/>s which caused the operation to fail.</param>
        /// <returns>An <see cref="ResponseResult"/> indicating a failed response operation, with a list of <paramref name="errors"/> if applicable.</returns>
        public static ResponseResult Failed(params ResponseError[] errors)
        {
            var result = new ResponseResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        public static ResponseResult Failed(params string[] errors)
        {
            var result = new ResponseResult { Succeeded = false };
            if (errors != null)
            {
                Array.ForEach(errors, item => { result._errors.Add(new ResponseError(item)); });
            }
            return result;
        }

        /// <summary>
        /// Creates an <see cref="ResponseResult"/> indicating a succeed response operation, with a list of <paramref name="result"/> if applicable.
        /// </summary>
        /// <param name="result">An optional param of <see cref="ResponseResult"/> which indicated the operation to success.</param>
        /// <returns>An <see cref="ResponseResult"/> indicating a succeed response operation, with a list of <paramref name="result"/> if applicable.</returns>
        public static ResponseResult Succeed(IEnumerable<object> results)
        {
            var response = new ResponseResult { Succeeded = true };
            if (results != null)
            {
                response._responses.AddRange(results);
            }
            return response;
        }

        /// <summary>
        /// Converts the value of the current <see cref="ResponseResult"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="ResponseResult"/> object.</returns>
        /// <remarks>
        /// If the operation was successful the ToString() will return "Succeeded" otherwise it returned 
        /// "Failed : " followed by a comma delimited list of error codes from its <see cref="Errors"/> collection, if any.
        /// </remarks>
        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Description).ToList()));
        }
    }

    /// <summary>
    /// Encapsulates an error from the response subsystem.
    /// </summary>
    public class ResponseError
    {
        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>
        /// <value>
        /// The code for this error.
        /// </value>
        public string Code { get; private set; } = "202";

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        /// <value>
        /// The description for this error.
        /// </value>
        public string Description { get; private set; }

        public ResponseError(string description)
        {
            Description = description;
        }

        public ResponseError(string code, string description)
        {
            Description = description;
            Code = code;
        }
    }
}
