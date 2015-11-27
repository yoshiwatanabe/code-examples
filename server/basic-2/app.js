var http = require('http');
var fs = require('fs');

function sendStatus404(response) {
  response.writeHead(404, { "Content-Type": "text/plain"});
  response.write("Not found");
  response.end();
}

function handleRequest(request, response) {
  console.log('request for ' + request.url);
  if (request.method == 'GET' && request.url == '/') {
    response.writeHead(200, { "Content-Type": "text/html"});
    fs.createReadStream('./index.html').pipe(response);
  }
  else {
    sendStatus404(response);
  }
}

http.createServer(handleRequest).listen(8080);
console.log("server started at http://localhost:8080");
