var canvas = document.getElementById('can'),
    context = canvas.getContext('2d');

var img = document.createElement('img');
img.onload = function () {
    context.drawImage(this,0,0);
};
img.src = 'http://www.planet-aye.co.uk/seasonal05/snow.png';
