var gulp = require('gulp');
var rimraf = require('gulp-rimraf');

// Delete the dist directory
gulp.task('clean', function() {
 console.log('cleaning...')
 return gulp.src('dist/')
 .pipe(rimraf());
});

gulp.task('default', ['clean']);
