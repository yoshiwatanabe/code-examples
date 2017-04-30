var readyStateCheckInterval = setInterval(function() {
  if (document.readyState === "complete") {
    clearInterval(readyStateCheckInterval);
    initialize();
  }
}, 10);

function person(first, last) {
    this.firstName = first;
    this.lastName = last;
}

person.prototype.age = 120;
person.prototype.fullName = function() {
  return this.firstName + " " + this.lastName + " (" + this.age + " years old)"
};

function initialize() {
  var p = Object.create(person.prototype);
  p.firstName = "yoshi"
  p.lastName = "watanabe"
  document.getElementById("content").textContent = p.fullName();
}
