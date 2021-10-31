﻿/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require("gulp");
var del = require("del");
var paths = {
    scripts: ["scripts/**/*.js", "scripts/**/*.ts", "scripts/**/*.map"],
};

gulp.task('hello', function () {
    console.log('Hello, World!, gulpfile.js');
    done();
});

gulp.task("gulp_clean_wwwroot", function () {
    return del(["wwwroot/scripts/**/*"]);
});

gulp.task("gulp_copy_wwwroot", function (done) {
    gulp.src(paths.scripts).pipe(gulp.dest("wwwroot/scripts"));
    done();
});
