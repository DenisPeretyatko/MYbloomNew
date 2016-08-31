var commonHub = function () {

    this.connection = {};
    var _this = {
        connection: this.connection,
    }

    this.LoginHub = function () {
        $.connection.hub.logging = true;
        
    }
  
    this.LogoutHub = function () {
        $.connection.hub.logging = false;
        $.connection.hub.disconnected();
    }
    
    this.GetConnection = function () {
        _this.connection = $.connection.bloomServiceHub;
        return _this.connection;
    }

    $.connection.hub.disconnected(function () {
        setTimeout(function () {
            $.connection.hub.start();
        }, 1000); 
    });
}
 
//commonHub.$inject = [];