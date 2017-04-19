var http = require('http');

function handleRequest(request, response) {
    console.log('request for ' + request.url);
    response.writeHead(200, { 'Content-Type': 'text/plain'});
    response.write('some text output');
    response.end();
}

http.createServer(handleRequest).listen(8080);
console.log("server started at http://localhost:8080");
