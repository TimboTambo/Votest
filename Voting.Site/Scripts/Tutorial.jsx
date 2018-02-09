var CommentBox = React.createClass({
    getInitialState: function() {
        return {data:0};
    },
    componentWillMount: function() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            this.setState({data: data});
        }.bind(this);
        xhr.send();
    },
    render: function() {
        return (
          <div className="commentBox">
            Hello, world! I am a CommentBox.
              {this.state.data}
          </div>
      );
    }
});
var CommentList = React.createClass({
    render: function() {
        return (
          <div className="commentList">
            Hello, world! I am a CommentList.
          </div>
      );
    }
});

var CommentForm = React.createClass({
    render: function() {
        return (
          <div className="commentForm">
            Hello, world! I am a CommentForm.
          </div>
      );
    }
});

ReactDOM.render(
  <CommentBox />,
  document.getElementById('content')
);