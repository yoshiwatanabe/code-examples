var gulp = require('gulp');

var paths = {
 scripts: ['app/scripts/**/*.js'],
 html: ['app/index.html', '!app/test.html'],
 dist: 'dist/'
};

gulp.task('default', function(){
 gulp.src(paths.scripts.concat(paths.html))
 .pipe(gulp.dest(paths.dist));
});
