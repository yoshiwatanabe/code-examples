var gulp = require('gulp');
var concat = require('gulp-concat');

gulp.task('compile', function() {
    return gulp.src('scripts/**/*.js')
        .pipe(concat('all.js'))
        .pipe(gulp.dest('dist'));
});

// Whenever any change is detected in a js file, we run the compile task.
gulp.task('watch', function() {
  gulp.watch('scripts/**/*.js', ['compile']);
});

// Default Task
gulp.task('default', ['compile', 'watch']);
