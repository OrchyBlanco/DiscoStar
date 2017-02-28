var HolaMundo = React.createClass({
    render : function(){
        return ( 
        <div> Hola {this.props.name}</div>
        );
    }
});

React.render(
    <HolaMundo name="Mundo"/>,
    document.getElementById('container')
);