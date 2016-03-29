var commonStateManager = function(commonDataService) {	
    this.profile = {};
    this.statistic = [];
    this.notifications = [];
    this.workorders = [];
    this.trucks = [];
    this.assigments = [];
    this.technicians = [];
    this.lookups = {};
    this.lookups.locations = [{
        id: 1,
        name: 'Location 1'  
    },
    {
        id: 2,
        name: 'Location 2'  
    },
    {
        id: 3,
        name: 'Location 3'  
    }];
    this.lookups.customers = [{
        id: 1,
        name: 'Customer 1'    
    },
    {
        id: 2,
        name: 'Customer 2'    
    }];
    this.lookups.calltypes = [{
        id: 1,
        name: 'Type 1'    
    },{
        id: 2,
        name: 'Type 2'    
    }];
    this.lookups.problems = [{

    }];
    this.lookups.ratesheets = [{
        id: 1,
        name: 'Rate 1'
    }]; 
    this.lookups.employes = [{
        id: 1,
        name: 'John Smith'
    }]; 
    this.lookups.equipment = [{
        id: 1,
        name: 'Drill'
    }]; 
    this.lookups.hours = [{
        id: 1,
        name: 'Default'
    }]; 
    this.lookups.paymentMethods = [{
        id: 1,
        name: 'Cash'
    },
    {
        id: 2,
        name: 'Wire Transfer'
    }]; 

}
commonStateManager.$inject = ["commonDataService"];