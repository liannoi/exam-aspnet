'use strict';

class Blur {
    constructor(selector, childSelector) {
        this.isBlured = false;
        this.selector = selector;
        this.childSelector = childSelector;
    }

    subscribeToBlur() {
        const self = this;
        this.selector.addEventListener('click',
            function() {
                this.blur();

                if (self.isBlured) {
                    self.childSelector.disabled = false;
                    self.isBlured = false;
                } else {
                    self.childSelector.disabled = true;
                    self.isBlured = true;
                }
            });
    }
}