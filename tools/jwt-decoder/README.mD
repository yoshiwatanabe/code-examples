#Simple JSON Web Token decoder

This is a node.js application that accepts a [base64url encoded JWT token](https://tools.ietf.org/html/draft-ietf-oauth-json-web-token-32) and prints out the header and claims in JSON.

##How to run

First, install the dependencies

```javascript
npm install
```

Then, run it

```javascript
node app.js <your base64url encoded JWT token>
```

If you don't specify the `<your base64url encoded JWT token>` at the command line, then the script will prompt you.
