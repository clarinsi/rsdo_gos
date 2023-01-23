;(function ( $, window, document, undefined ) {


    // Create the defaults once
    var pluginName = "dropdown",
        defaults = {
            propertyName: "value"
        };

    // The actual plugin constructor
    function Plugin( element, options ) {
        this.element = element;

        // jQuery has an extend method that merges the
        // contents of two or more objects, storing the
        // result in the first object. The first object
        // is generally empty because we don't want to alter
        // the default options for future instances of the plugin
        this.options = $.extend( {}, defaults, options) ;

        this._defaults = defaults;
        this._name = pluginName;

        this.init();
    }

    Plugin.prototype.init = function () {
        // Place initialization logic here
        // We already have access to the DOM element and
        // the options via the instance, e.g. this.element
        // and this.options
        this.bindActions();
        var $dropdown = $(this.element);
        //init first choice by default
        $dropdown.find('.dropdown-value').text($('.dropdown-list li:first',$dropdown).text());

        var initialValue = $('.dropdown-list li:first',$dropdown).attr('data-value');
        $dropdown.find('.dropdown-value').attr('data-value', initialValue);
        $dropdown.find('input[type="hidden"]').val(null);
        $dropdown.find('input[type="hidden"]:first').val(initialValue);
        
        $('.dropdown-list li:first',$dropdown).addClass('hidden');
    };


    Plugin.prototype.bindActions = function () {
        // Place initialization logic here
        // We already have access to the DOM element and
        // the options via the instance, e.g. this.element
        // and this.options
        var $dropdown = $(this.element);
        $dropdown.on('click','.dropdown-field', function() {
            var $el = $(this).closest('.dropdown');
            if($el.hasClass('open')) {
                $el.removeClass('open');
            } else {
                $el.addClass('open');
            }
        });

        $(document).mouseup(function(e)
        {
            // if the target of the click isn't the container nor a descendant of the container
            if (!$dropdown.is(e.target) && $dropdown.has(e.target).length === 0)
            {
                $dropdown.removeClass('open');
            }
        });

        $dropdown.on('click','.dropdown-list li', function() {
            $dropdown.find('.dropdown-list li').removeClass('hidden');
            $(this).addClass('hidden');

            var value = $(this).attr('data-value');
            var text = $(this).attr('data-text');
            
            // reset all values of dropdown (needed for PartOfSpeech dropdown)
            $dropdown.find('input[type="hidden"]').val(null);
            
            // set values on a first hidden field (needed for PartOfSpeech dropdown)
            $dropdown.find('input[type="hidden"]:first').val(value);
            
            $dropdown.find('.dropdown-value').text(text);
            $dropdown.find('.dropdown-value').attr('data-value', value);
            $dropdown.trigger('change');
            $dropdown.removeClass('open');
        });
    };

    // A really lightweight plugin wrapper around the constructor,
    // preventing against multiple instantiations
    $.fn[pluginName] = function ( options ) {
        return this.each(function () {
            if ( !$.data(this, "plugin_" + pluginName )) {
                $.data( this, "plugin_" + pluginName,
                    new Plugin( this, options ));
            }
        });
    }

})(jQuery, window, document);
