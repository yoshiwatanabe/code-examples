var express = require('express');
var app = express();

app.get('/user', function(req, res) {

  var data = {
    "user1": {
      "name": "alice",
      "id": 1
    },
    "user2": {
      "name": "bob",
      "id": 2
    },
    "user3": {
      "name": "chris",
      "id": 3
    }
  };

  res.json(data);

});

var server = app.listen(8080, function() {
  var host = server.address().address;
  var port = server.address().port;

  console.log('Example app listening at http://%s:%s', host, port);
});
