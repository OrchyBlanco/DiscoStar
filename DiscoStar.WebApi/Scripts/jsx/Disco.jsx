var CajaDiscoTop = React.createClass({
    render : function(){
        return (
                <div className="discosTop">
                    Hola
                </div>
            );
    }
});

var CajaDisco=React.createClass({
    render : function(){
        return (
                <div className="discosTop">
                    Hola
                </div>
            );
    }
});

React.render(
    <CajaDiscoTop/>,document.getElementById('content')
    
    );