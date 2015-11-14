var gulp = require('gulp');
var jshint = require('gulp-jshint');

var paths = {
 scripts: ['scripts/**/*.js', '!scripts/libs/**/*.js']
};

// Lint Task
gulp.task('lint', function() {
    return gulp.src(paths.scripts)
        .pipe(jshint())
        .pipe(jshint.reporter('default'));
});

// Default Task
gulp.task('default', ['lint']);
