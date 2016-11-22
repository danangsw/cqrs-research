/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var ts = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');
var livereload = require('gulp-livereload');


var root_path = {
    webroot: "./wwwroot/",
    nodemodules: "./node_modules/",
}

root_path.app = root_path.webroot + "app/";
root_path.webrootcss = root_path.webroot + "css/";
root_path.webrootjs = root_path.webroot + "js/";
root_path.webrootvendorjs = root_path.webroot + "js/app/vendor";
root_path.webrootlib = root_path.webroot + "lib/";


gulp.task("copy-bootstrap", function () {
    return gulp.src(root_path.nodemodules + 'bootstrap/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'bootstrap/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'bootstrap'));

});


gulp.task("copy-jquery", function () {
    return gulp.src(root_path.nodemodules + 'jquery/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'jquery/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'jquery'));

});

gulp.task("copy-jquery-validation-unobtrusive", function () {
    return gulp.src(root_path.nodemodules + 'jquery-validation-unobtrusive/' + '**/*.*', {
        base: root_path.nodemodules + 'jquery-validation-unobtrusive/'
    }).pipe(gulp.dest(root_path.webrootlib + 'jquery-validation-unobtrusive'));

});

gulp.task("copy-jquery-validation", function () {
    return gulp.src(root_path.nodemodules + 'jquery-validation/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'jquery-validation/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'jquery-validation'));

});

gulp.task("copy-admin-lte", function () {
    return gulp.src(root_path.nodemodules + 'admin-lte/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'admin-lte/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'admin-lte'));

});

gulp.task("copy-angular", function () {
    return gulp.src(root_path.nodemodules + 'angular/' + '**/*.*', {
        base: root_path.nodemodules + 'angular/'
    }).pipe(gulp.dest(root_path.webrootlib + 'angular'));

});

gulp.task("copy-font-awesome", function () {
    return gulp.src(root_path.nodemodules + 'font-awesome/' + '**/*.*', {
        base: root_path.nodemodules + 'font-awesome/'
    }).pipe(gulp.dest(root_path.webrootlib + 'font-awesome'));

});

gulp.task("copy-ionicons", function () {
    return gulp.src(root_path.nodemodules + 'ionicons/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'ionicons/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'ionicons'));

});

gulp.task("copy-fastclick", function () {
    return gulp.src(root_path.nodemodules + 'fastclick/lib/' + '**/*.*', {
        base: root_path.nodemodules + 'fastclick/lib/'
    }).pipe(gulp.dest(root_path.webrootlib + 'fastclick'));

});

gulp.task("copy-jquery-slimscroll", function () {
    return gulp.src(root_path.nodemodules + 'jquery-slimscroll/' + '**/*.*', {
        base: root_path.nodemodules + 'jquery-slimscroll/'
    }).pipe(gulp.dest(root_path.webrootlib + 'jquery-slimscroll'));

});


gulp.task("copy-angular-ui-bootstrap", function () {
    return gulp.src(root_path.nodemodules + 'angular-ui-bootstrap/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'angular-ui-bootstrap/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'angular-ui-bootstrap'));

});


gulp.task("copy-angular-amd", function () {
    return gulp.src(root_path.nodemodules + 'angular-amd/' + '**/*.*', {
        base: root_path.nodemodules + 'angular-amd/'
    }).pipe(gulp.dest(root_path.webrootlib + 'angular-amd'));

});

gulp.task("copy-requirejs", function () {
    return gulp.src(root_path.nodemodules + 'requirejs/' + '**/*.*', {
        base: root_path.nodemodules + 'requirejs/'
    }).pipe(gulp.dest(root_path.webrootlib + 'requirejs'));

});


gulp.task("copy-requirejs-domready", function () {
    return gulp.src(root_path.nodemodules + 'requirejs-domready/' + '**/*.*', {
        base: root_path.nodemodules + 'requirejs-domready/'
    }).pipe(gulp.dest(root_path.webrootlib + 'requirejs-domready'));

});

gulp.task("copy-angular-validation", function () {
    return gulp.src(root_path.nodemodules + 'angular-validation/dist/' + '**/*.*', {
        base: root_path.nodemodules + 'angular-validation/dist/'
    }).pipe(gulp.dest(root_path.webrootlib + 'angular-validation'));

});

gulp.task("copy-icheck", function () {
    return gulp.src(root_path.nodemodules + 'icheck/' + '**/*.*', {
        base: root_path.nodemodules + 'icheck/'
    }).pipe(gulp.dest(root_path.webrootlib + 'icheck'));

});

gulp.task("copy-icheck", function () {
    return gulp.src(root_path.nodemodules + 'icheck/' + '**/*.*', {
        base: root_path.nodemodules + 'icheck/'
    }).pipe(gulp.dest(root_path.webrootlib + 'icheck'));

});

gulp.task("copy-typescript-collections", function () {
    return gulp.src(root_path.nodemodules + 'typescript-collections/dist/lib/' + '**/*.*', {
        base: root_path.nodemodules + 'typescript-collections/dist/lib/'
    }).pipe(gulp.dest(root_path.webrootvendorjs + 'collections'));

});





gulp.task("copy-libs", ["copy-bootstrap", 'copy-jquery', 'copy-jquery-validation-unobtrusive',
    'copy-jquery-validation', 'copy-jquery-validation', 'copy-admin-lte', 'copy-angular', 'copy-font-awesome',
    'copy-ionicons', 'copy-fastclick', 'copy-jquery-slimscroll', 'copy-angular-ui-bootstrap', 'copy-angular-amd', 'copy-requirejs',
    'copy-requirejs-domready', 'copy-icheck', 'copy-angular-validation']);

var tsProject = ts.createProject('tsconfig.json');
gulp.task('ts', function (done) {
    var tsResult = gulp.src([
            "./wwwroot/js/app/**/*.ts"
    ]).pipe(sourcemaps.init())
        .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());

    return tsResult.js.pipe(sourcemaps.write())
    .pipe(gulp.dest('./wwwroot/js/app')).pipe(livereload());
});

gulp.task('watch.ts', ['ts'], function () {
    livereload.listen();
    return gulp.watch('./wwwroot/js/**/*.ts', ['ts']);
});

gulp.task('watch', ['watch.ts']);

