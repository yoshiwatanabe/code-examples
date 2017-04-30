var prompt = require('prompt');
var util = require('util')

// Obtain a token from command line.
if (process.argv.length < 3) {
  console.log('Enter base64-encoded JSON Web Token >');
  prompt.start();
  prompt.get(['token'], function (err, result) {
    if (err) {
      return onErr(err);
    }

    decodeToken(result.token);
  });

  function onErr(err) {
    console.log(err);
    return 1;
  }
} else {
  decodeToken(process.argv[2]);
}

// Decode a JWT token to a plain JSON.
function decodeToken(base64token) {
  var sections = base64token.split(".");
  if (sections.length != 3) {
    console.log('Invalid input token. Expecting a string with two periods.');
    return;
  }

  var headerBuf = new Buffer(sections[0], 'base64');
  console.log('\nheader\n');
  console.log(util.inspect(JSON.parse(headerBuf.toString("ascii")), {depth: null, colors: true}))

  console.log('\nclaims\n');
  var claimsBuf = new Buffer(sections[1], 'base64');
  console.log(util.inspect(JSON.parse(claimsBuf.toString("ascii")), {depth: null, colors: true}))
  console.log('\n');
}
