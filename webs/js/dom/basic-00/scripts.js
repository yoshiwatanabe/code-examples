// http://www.w3.org/TR/2011/WD-html5-20110525/timers.html#dom-windowtimers-setinterval
var readyStateCheckInterval = setInterval(function() {
  // http://www.w3.org/TR/2011/WD-html5-20110525/dom.html#dom-document-readystate
  if (document.readyState === "complete") {
    // http://www.w3.org/TR/2011/WD-html5-20110525/timers.html#dom-windowtimers-clearinterval
    clearInterval(readyStateCheckInterval);
    initialize();
  }
}, 10);

function initialize() {
  // https://developer.mozilla.org/en-US/docs/Web/API/Document/getElementById
  // https://developer.mozilla.org/en-US/docs/Web/API/Node/textContent
  document.getElementById("content").textContent = "hello!!"
}
