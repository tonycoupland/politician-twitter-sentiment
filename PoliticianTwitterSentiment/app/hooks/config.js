"use strict";

var routeMap;

routeMap = require("../routes.js");

module.exports = /* @ngInject */ function($stateProvider, $urlRouterProvider) {
  angular.forEach(routeMap, function(route) {
    var state;
    if (typeof route.otherwise !== "undefined") {
      $urlRouterProvider.otherwise(route.otherwise);
    }
    state = route.url;
    if (route.state) {
      state = route.state;
      delete route.state;
    }
    $stateProvider.state(state, route);
  });
};
