// When a JS file is required, the exports of the required file is brought in

const util = require("./utilModule.js");

console.log("In Main Test 1", module);
console.log("Things brought in from utilModule.", util)