var gulp = require('gulp');
var rimraf = require('gulp-rimraf');

var bases = {
 app: 'app/',
 dist: 'dist/',
};

// Delete the dist directory
gulp.task('clean', function() {
 console.log('cleaning...')
 return gulp.src(bases.dist)
 .pipe(rimraf());
});

gulp.task('default', ['clean']);
