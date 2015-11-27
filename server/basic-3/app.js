var http = require('http');
var connect = require('connect');

var app = connect();

app.use('/foo', function(request, response) {
  response.writeHead(200, { 'Content-Type':'text/plain'});
  response.write('foo');
  response.end();
})

app.use('/bar', function(request, response){
  response.writeHead(200, { 'Content-Type' : 'text/plain'});
  response.write('bar');
  response.end();
});

http.createServer(app).listen(8080);
console.log("server started at http://localhost:8080");
