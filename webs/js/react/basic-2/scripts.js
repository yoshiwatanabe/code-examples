
var GreetingWithTime = React.createClass({
  render: function() {
    return (
      <p>
        Hello, <span>{this.props.name}</span>. It is {this.props.date.toTimeString()}
      </p>
    );
  }
});

setInterval(function() {
  ReactDOM.render(
    <GreetingWithTime name='yoshi' date={new Date()} />, document.getElementById('container')
  );
}, 500);
