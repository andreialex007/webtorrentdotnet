require.config({
    baseUrl: "/",
    paths: {
        'jquery': 'Metronic/global/plugins/jquery-1.11.0.min',
        "jquery.ui.widget": "Scripts/libs/jquery.ui.widget",
        "angular": 'Scripts/libs/angular-1.3.3/angular',
        "angular-route": 'Scripts/libs/angular-1.3.3/angular-route',
        "angular-resource": 'Scripts/libs/angular-1.3.3/angular-resource',
        "signalr": 'Scripts/libs/jquery.signalr-2.1.0',
        "signalr-proxies": 'signalr/js?noext'
    },
    shim: {
        'jquery': {
            exports: ['$']
        },
        'Metronic/global/plugins/jquery-migrate-1.2.1.min': {
            deps: ['jquery']
        },
        'Metronic/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min': {
            deps: ['jquery']
        },
        'Metronic/global/plugins/bootstrap-select/bootstrap-select': {
            deps: ['Metronic/global/plugins/bootstrap/js/bootstrap.min', 'jquery']
        },
        'Metronic/global/plugins/bootstrap/js/bootstrap.min': {
            deps: ['jquery']
        },
        'Metronic/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min': {
            deps: ['jquery', 'Metronic/global/plugins/bootstrap/js/bootstrap.min']
        },
        'Metronic/global/plugins/jquery-slimscroll/jquery.slimscroll.min': {
            deps: ['jquery']
        },
        'Metronic/global/plugins/jquery.blockui.min': {
            deps: ['jquery']
        },
        'Scripts/libs/jquery.numeric': {
            deps: ['jquery']
        },
        'Scripts/libs/jquery.inputmask.bundle': {
            deps: ['jquery']
        },
        'Scripts/libs/jquery.maskedinput': {
            deps: ['jquery']
        },
        'Scripts/libs/jquery-imask-min': {
            deps: ['jquery']
        },
        'Metronic/global/plugins/jquery.cokie.min': {
            deps: ['jquery']
        },
        'Metronic/global/plugins/uniform/jquery.uniform.min': {
            deps: ['jquery']
        },
        'signalr': {
            deps: ['jquery']
        },
        'signalr-proxies': {
            deps: ['jquery', "signalr"]
        },
        'Metronic/global/plugins/bootstrap-switch/js/bootstrap-switch.min': {
            deps: ['jquery', 'Metronic/global/plugins/bootstrap/js/bootstrap.min']
        },
        'Metronic/global/scripts/metronic': {
            deps: ['jquery'],
            exports: 'Metronic'
        },
        'Metronic/global/plugins/select2/select2': {
            deps: ['jquery']
        },
        'Metronic/admin/layout/scripts/layout': {
            deps: ['Metronic/global/scripts/metronic']
        },
        'Metronic/admin/layout/scripts/quick-sidebar': {
            deps: ['Metronic/global/scripts/metronic']
        },
        'Scripts/libs/jquery.ui.widget': {
            deps: ['Metronic/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min', 'jquery']
        },
        'Scripts/libs/jquery.iframe-transport': {
            deps: ['jquery.ui.widget']
        },
        'Scripts/libs/jquery.fileupload': {
            deps: ['Scripts/libs/jquery.iframe-transport', 'jquery.ui.widget']
        },
        'angular-route': {
            deps: ['angular']
        },
        'angular-resource': {
            deps: ['angular']
        },

    }
});


require(['Scripts/app.js'], function () {
});