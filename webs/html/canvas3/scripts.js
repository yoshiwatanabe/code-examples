var canvas = document.getElementById('can'),
    context = canvas.getContext('2d');

//canvas.addEventListener('mousedown', function() { alert('hi') }, false);

var img = document.createElement('img');
//document.body.appendChild(img);
img.src = 'http://www.planet-aye.co.uk/seasonal05/snow.png';
img.onload = function () {
    context.drawImage(this,0,0);
};

img.addEventListener('mousedown', function() { alert('imgsss') }, false);

var ribbon = document.createElement('img');
ribbon.style.backgroundColor = "#000000";
ribbon.style.height = 100;
document.body.appendChild(ribbon);
ribbon.src= 'http://pngimg.com/upload/ribbon_PNG1557.png';
//ribbon.onload = function()  {
//  context.drawImage(this, 10, 100);
//}

ribbon.addEventListener('mousedown', function() { alert('hi') }, false);
