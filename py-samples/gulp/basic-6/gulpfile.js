var gulp = require('gulp');
var rimraf = require('gulp-rimraf');
var connect = require('gulp-connect');

gulp.task('copy', function(){
 gulp.src(['app/**/*.*'])
 .pipe(gulp.dest('dist/'));
});

gulp.task('watch', function() {
  gulp.watch('app/**/*.*', ['copy']);
});

gulp.task('serve', function(){
  connect.server({root: './dist'});
});

gulp.task('default', ['copy','watch','serve']);
