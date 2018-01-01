(function($, ns) {
	'use strict';

	ns.SubwayStopPageHandler = function() {
	    var me = this,
	        urls = {
	            apiPrefix: '/api/subwaystops',
	            getSubwayStops: '',
	            calculateDistance: '/distance'
	        },
			selectors = {
	            originTextBox: '#origin',
	            destinationTextBox: '#destination',
	            typeahead: '.typeahead',
	            button: '#calculate-button',
	            message: '#message',
	            form: '#form',
	            responseTemplate: '#response-template'
			};

		function initTypeaheads(data) {
			$(selectors.typeahead).typeahead({
				source: data,
			    autoSelect: true
		    }).prop('disabled', false);
		}

		function validateInput() {
		    var originVal = $(selectors.originTextBox).val(),
		        destinationVal = $(selectors.destinationTextBox).val(),
		    	calculateButton= $(selectors.button);

			if (originVal && destinationVal) {
				calculateButton.prop('disabled', false);
			    return true;
			} else {
				calculateButton.prop('disabled', 'disabled');
			    return false;
			}
		}

		function applyModelToTemplate(model) {
		    var prop,
		        templateValue = $(selectors.responseTemplate).html();

		    if (model !== null && model !== undefined) {
		        for (prop in model) {
		            if (model.hasOwnProperty(prop)) {
		                templateValue = templateValue.replace('{{' + prop + '}}', model[prop]);
		            }
		        }
		    }

		    return templateValue;
		}

		function formSubmitHandler(event) {
		    requestDistance();
		    event.preventDefault();
		}

		function requestDistance() {
			var origin = $(selectors.originTextBox),
		        destination = $(selectors.destinationTextBox),
		        activeOrigin,
		        activeDestination;

		    if (validateInput()) {
		        activeOrigin = origin.typeahead('getActive');
		        activeDestination = destination.typeahead('getActive');

		        if (activeOrigin.hasOwnProperty('id') && activeDestination.hasOwnProperty('id')) {
		            $(selectors.message).text('loading...');
		            $.ajax({
		                type: "POST",
		                url: urls.apiPrefix + urls.calculateDistance,
		                dataType: 'json',
		                data: JSON.stringify({
		                    originSubwayStopId: activeOrigin.id,
		                    destinationSubwayStopId: activeDestination.id
		                }),
		                contentType: 'application/json'
		            }).done(function (data) {
		                var template = applyModelToTemplate(data);
		                $(selectors.message).html(template);
		            }).fail(function (xhr, error) {
		                $(selectors.message).text("An error occurred requesting the information. Error: " + error);
		            });
		        }
		    }
		}

		function attachHandlers() {
		    $(selectors.originTextBox + ',' + selectors.destinationTextBox).change(function() {
		    	$(selectors.message).text('');
		        validateInput();
		    });

		    $(selectors.form).on('submit', formSubmitHandler);
		}

		me.init = function () {
			// init typeaheads
		    $(selectors.message).text('loading...');
	        $.get(urls.apiPrefix + urls.getSubwayStops,
	            function(data) {
	            	initTypeaheads(data);
	                $(selectors.message).text('');
	            });

	        attachHandlers();
	    };

	    return me;
	}

})(window.jQuery, Ipreo.getNamespace('Ipreo.PageHandlers'));