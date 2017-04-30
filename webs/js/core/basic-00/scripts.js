var readyStateCheckInterval = setInterval(function() {
  if (document.readyState === "complete") {
    clearInterval(readyStateCheckInterval);
    initialize();
  }
}, 10);

// http://people.mozilla.org/~jorendorff/es6-draft.html#sec-ecmascript-function-objects
function person(first, last) {
    // http://people.mozilla.org/~jorendorff/es6-draft.html#sec-this-keyword
    this.firstName = first;
    this.lastName = last;
}

function initialize() {
  // http://www.ecma-international.org/ecma-262/6.0/#sec-new-operator
  var p = new person("yoshi", "watanabe");
  p.age = 12;
  p.fullName = function() {
    return this.firstName + " " + this.lastName + " (" + this.age + " years old)"
  };
  document.getElementById("content").textContent = p.fullName();
}
