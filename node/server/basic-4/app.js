var http = require('http');
var connect = require('connect');

var app = connect();

function first(request, response, next){
  console.log('first');
  response.writeHead(200, { 'Content-Type' : 'text/plain'});
  response.write('first');
  //response.end();
  next();
}

function second(request, response, next){
  console.log('second');
  response.write('second');
  response.end();
  next();
}

app.use(first);
app.use(second);

http.createServer(app).listen(8080);
console.log("server started at http://localhost:8080");
