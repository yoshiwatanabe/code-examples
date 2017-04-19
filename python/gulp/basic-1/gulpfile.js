var gulp = require('gulp');

gulp.task('default', function(){
 gulp.src(['app/index.html','app/scripts/*.js'])
 .pipe(gulp.dest('dist/'));
});
