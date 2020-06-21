const lib = require('./library');

const tax = 0.1
let price = 100.00;

console.log(lib.add(price, lib.multiply(price, tax)));