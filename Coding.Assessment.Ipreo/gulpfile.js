/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    uglify = require('gulp-uglify'),
    uglifycss = require('gulp-uglifycss'),
    sourcemaps = require('gulp-sourcemaps');

gulp.task('default', function () {
    // place code for your default task here
    gulp.src([ 
        'app/scripts/app.js',
        'app/scripts/subwayStopPageHandler.js'])
        .pipe(sourcemaps.init())
        .pipe(concat('app.js'))
        .pipe(rename('app.min.js'))
        .pipe(uglify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('app/dist'));

    gulp.src('app/content/*.css')
        .pipe(uglifycss({
            "maxLineLen": 80,
            "uglyComments": true
        }))
        .pipe(gulp.dest('app/dist/'));
});