var gulp = require('gulp');
var connect = require('gulp-connect');
var rimraf = require('gulp-rimraf');

gulp.task('clean', function() {
 return gulp.src('dist/**/*.*')
 .pipe(rimraf());
});

gulp.task('copy', function(){
 gulp.src(['app/index.html','app/scripts/*.js'])
 .pipe(gulp.dest('dist/'));
});

gulp.task('watch', function() {
  gulp.watch('app/**/*.*', ['copy']);
});

gulp.task('default', ['clean','copy','watch']);
