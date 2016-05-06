var commonNotificationService = function() {

	this.info = function(message) {
		toaster.pop({
            type: 'info',
            title: 'Warning',
            body: message,
            showCloseButton: true
        });
	}

	this.warn = function(message) {
		toaster.pop({
            type: 'error',
            title: 'Warning',
            body: message,
            showCloseButton: true
        });
	}	
	
}
commonNotificationService.$inject = [];