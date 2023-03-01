import './../Styles/navbar.scss';
import './../Styles/footer.scss';
import './../Styles/styles_new.scss';
import './../Styles/phone.scss';
import './../Styles/index.scss';
import './../Styles/about.scss';

import './../Styles/gos-common.scss';
import './../Styles/gos-forms.scss';
import './../Styles/gos-concordance-advanced.scss';
import './../Styles/gos-common-index.scss';
import './../Styles/gos-list-index.scss';
import './../Styles/gos-common-results.scss';
import './../Styles/gos-concordance-results.scss';
import './../Styles/gos-list-results.scss';
import './../Styles/gos-pos.scss';

// Import legacy scripts
import dropdown from './dropdown.exec.js';

// History
const History = {
    getHistory: function (key) {
        return localStorage.getItem(key) ? JSON.parse(localStorage.getItem(key)) : [];
    },

    getLastHistory: function (key) {
        return History.getHistory(key).slice(0, 10);
    },

    saveHistoryItem: function (key, historyItem) {
        const history = History.getHistory(key);
        const historyItemJSON = JSON.stringify(historyItem);

        // Add only if not already in array
        if (!(history.some(e => JSON.stringify(e) === historyItemJSON))) {
            history.unshift(historyItem);
            localStorage.setItem(key, JSON.stringify(history));
        }
    }
}

const Pos = {
    showDetails: function (partOfSpeechId, callback) {
        $('.pos-holder').load(posUrl + '?Id=' + encodeURIComponent(partOfSpeechId),
            function () {
                $('.pos-holder').show();

                $('.pos-holder button.primary').click(function () {
                    const values = [];
                    $('.pos-holder input[type="checkbox"]:checked').each(function () {
                        values.push($(this).val());
                    });

                    Pos.closeDetails();
                    callback(values);
                    return false;
                });

                $('.pos-holder button.clear').click(function () {
                    $('.pos-holder input[type="checkbox"]').prop('checked', false);
                    return false;
                });

                $('.pos-holder .btn-exit').click(function () {
                    Pos.closeDetails();
                    return false;
                });
            });
    },

    closeDetails: function () {
        $('.pos-holder').hide();
    }
}

const Utils = {
    changeImageSrc: function (el, newFileName) {
        const src = el.attr("src"); // get the src
        const path = src.substring(0, src.lastIndexOf('/')); // get the path from the src 
        const newSrc = path + '/' + newFileName; // re-assemble new src
        el.attr("src", newSrc);
    }
}

const Gos = {
    init: function () {
        Gos.bindCommon();
        Gos.bindHistory();
        Gos.bindIndex();
        Gos.bindResults();
        Gos.bindConcordance();
        Gos.bindConcordanceAdvanced();
        Gos.bindConcordanceSearchResults();
        Gos.bindList();
        Gos.bindListIndex();
        Gos.bindListSearchResults();
        Gos.bindPlay();
        Gos.bindViri();
    },

    bindCommon: function () {
        // Menus
        $('body.index .search-options .form a').click(function () {
            const form = $(this).data('form');
            $('#TranscriptionType').val(form);

            $('body.index .search-options .form a').removeClass('active');
            $(this).addClass('active');
            return false;
        });

        // Social
        $(document).on('click touch', '#share-fb', function (e) {
            e.preventDefault();
            window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(window.location.href) + "&fbrefresh=true", 'facebook-share-dialog', 'width=626,height=436');
            return false;
        });
        $(document).on('click touch', '#share-twitter', function (e) {
            e.preventDefault();
            window.open('https://twitter.com/share?url=' + encodeURIComponent(window.location.href) + '&text=Gos 2.0' + '&hashtags=cjvt,gos20', '', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=300,width=600');
            return false;
        });

        // Closing drop down menus
        $(document).click(function (e) {
            const target = $(e.target);
            if (!target.closest('#posIcon').length && !target.closest('.search-dropdown.pos').length && $('.search-dropdown.pos').is(':visible')) {
                $('.search-input').removeClass('autocomplete');
                $('.search-dropdown.pos').hide();
            }
            if (!target.closest('#historyIcon').length && !target.closest('.search-dropdown.history').length && $('.search-dropdown.history').is(':visible')) {
                $('.search-input').removeClass('autocomplete');
                $('.search-dropdown.history').hide();
            }
        });
    },

    bindHistory: function () {
        $('.search-input #historyIcon a').click(function () {
            const historyUrl = $(this).data('history-url');
            const historyKey = $(this).data('history-key');
            const history = History.getLastHistory(historyKey);

            $.ajax(historyUrl, {
                contentType: 'application/json; charset=utf-8',
                dataType: 'html',
                method: 'POST',
                data: JSON.stringify(history),
                success: function (data) {
                    $('.search-input').addClass('autocomplete');
                    $('.search-dropdown.history').html(data);
                    $('.search-dropdown.history').show();
                }
            });
            return false;
        });
    },

    bindIndex: function () {
        if (!($('body').hasClass('index'))) {
            return;
        }

        // Inputs
        $('.search .search-input').on('click touch', function () {
            $('input:text', $(this)).val('').focus();
            $(this).closest('.search').find('.search-autocomplete').hide();
        });

        // If there is no autocomplete than focus search field
        if ($('.search .search-autocomplete').length === 0) {
            $('#Query').focus();
        }

        // Help
        $('a.show-help').click(function () {
            $('.search-help .help').show();
            return false;
        });

        $('.help .btn-exit').click(function () {
            $('.help').hide();
            return false;
        });
    },

    bindResults: function () {
        if (!($('body').hasClass('results'))) {
            return;
        }

        const navigate = function (search, transcriptionType) {
            const query = $('#Query').val();

            // Save search to history
            const historyKey = $(this).data('history-key');
            const historyItem = {query: query};
            History.saveHistoryItem(historyKey, historyItem);

            // Navigate to new search
            const url = search + '?query=' + encodeURIComponent(query) + '&TranscriptionType=' + encodeURIComponent(transcriptionType);
            window.location = url;
            return false;
        };

        $('.search-input .area a').click(function () {
            const search = $(this).data('search');
            const transcriptionType = $('.search-input .form a.active').data('transcription-type');
            navigate(search, transcriptionType);
        });

        $('.search-input .form a').click(function () {
            const search = $('.search-input .area a.active').data('search');
            const transcriptionType = $(this).data('transcription-type');
            navigate(search, transcriptionType);
        });

        // Inputs
        $('.search .search-input').on('click touch', function (e) {
            e.preventDefault();
            $('.search-text', $(this)).hide();

            // Move cursor to the end of th einput
            const input = $(this).find('input[type="text"]');
            const old = input.val();
            input.show().focus().val('').val(old);
        });

        // Aggregations
        $(document).on('mouseenter', '.filter-category',
            function (e) {
                e.stopPropagation();
                $(this).parent().mouseleave();
                $(this).addClass('hover');
            });

        $(document).on('mouseleave', '.filter-category',
            function (e) {
                e.stopPropagation();
                $(this).parent().mouseenter();
                $(this).removeClass('hover');
            });

        $('.filter-category').click(function () {
            const url = $(this).data('url');
            if (url) {
                window.location.href = url;
            }
        });

        $('.filter-category.expand').click(function () {
            $(this).closest('.filters-section').removeClass('collapsed');
            $(this).closest('.filters-section').addClass('expanded');
        });

        $('.filter-category.collapse').click(function () {
            $(this).closest('.filters-section').removeClass('expanded');
            $(this).closest('.filters-section').addClass('collapsed');
        });

        // Export
        $('#export').click(function () {
            $('.module.export').show();
            return false;
        });

        $('.module.export input[type="text"]').change(function () {
            console.log('on change');
            const total = $('.module.export').data('total');
            const count = $(this).val();
            if (total > 1000 && count > 1000) {
                $('.module.export .max-records').show();
            } else {
                $('.module.export .max-records').hide();
            }
        });

        $('.module.export .btn-exit').click(function () {
            $('.module.export').hide();
            return false;
        });

        // Pager
        $('.pager .active > a').click(function () {
            const parent = $(this).parent();
            $(this).hide();
            const input = parent.find('.page-input');
            input.show();

            const textInput = input.find('input');
            textInput.focus();

            // Workaround for setting cursor to the end of the input
            const tmpValue = textInput.val();
            textInput.val('');
            textInput.val(tmpValue);
            return false;
        });

        $('.pager .page-input a').click(function () {
            const parent = $(this).parent();
            const page = parent.find('input').val();
            if (!(isNaN(page))) {
                const from = (page - 1) * 20;
                const url = $(this).attr('href').replace("@@FROM@@", from);
                window.location.href = url;
            }
            return false;
        });

        $('.pager .page-input input').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                $(this).siblings('a').click();
            }
        });
    },

    bindConcordance: function () {
        if (!($('body').hasClass('concordance'))) {
            return;
        }

        // Submit search
        $('.search-input form').submit(function () {
            const input = $(this).find('input:text');
            const query = input.val();
            const transcriptionType = $('#TranscriptionType').val();
            if ($.trim(query).length === 0) {
                input.focus();
                return false;
            }

            // Save to history
            const historyKey = 'concordance-search-history';
            const historyItem = {query: query, transcriptionType: transcriptionType};
            History.saveHistoryItem(historyKey, historyItem);
        });
    },

    bindConcordanceAdvanced: function () {
        if (!($('body').hasClass('concordance') && $('body').hasClass('advanced'))) {
            return;
        }

        $('.dropdown').dropdown();

        $('.word-selector').click(function () {
            $(this).closest('.word-input').toggleClass('expanded');
        });

        $('.word-input .dropdown-list li').click(function () {
            const value = $(this).data('value');
            const wordInput = $(this).closest('.word-input');
            wordInput.find('input').val(value);
            wordInput.removeClass('expanded');
        });

        // Part of speech
        const bindPartOfSpeech = function (owner) {
            $(owner).find('.pos ul li button').click(function () {
                const partOfSpeechId = $(this).closest('li').data('value');
                Pos.showDetails(partOfSpeechId, function (values) {
                    $(owner).find('input[name*="Msds"]').val(values);
                });
            });
        };

        // Bind part of speech for main word
        bindPartOfSpeech($('.content.word'));

        const regenerateNamesForWordInContext = function (word, cnt) {
            word.find('input, select, label').each(function () {
                const obj = $(this);

                const oldName = obj.attr('name');
                if (oldName) {
                    const newName = oldName.replace(/Original|\[\d+\]/, '[' + cnt + ']');
                    obj.attr('name', newName);
                }

                const oldId = obj.attr('id');
                if (oldId) {
                    const newId = oldId.replace(/Original|\[\d+\]/, '[' + cnt + ']');
                    obj.attr('id', newId);
                }

                const oldFor = obj.attr('for');
                if (oldFor) {
                    const newFor = oldFor.replace(/Original|\[\d+\]/, '[' + cnt + ']');
                    obj.attr('for', newFor);
                }
            });
        };

        const regenerateNames = function (element) {
            // Regenerate names for all words in context
            if (!element) {
                let cnt = 0;
                $('.word-in-context:not(.original)').each(function () {
                    regenerateNamesForWordInContext($(this), cnt);
                    cnt = cnt + 1;
                });
            } else {
                const cnt2 = $('.word-in-context:not(.original)').length;
                regenerateNamesForWordInContext(element, cnt2);
            }
        };

        const setAddWordLink = function () {
            const link = $('.add-word');
            const words = $('.word-in-context:not(.original)').length;

            if (words === 0) {
                link.show();
                link.html(link.data('add-text'));
            } else if (words < 3) {
                link.show();
                link.html(link.data('additional-text'));
            } else {
                link.hide();
            }
        };

        const bindEvents = function (word) {
            word.find('button.remove').click(function () {
                word.remove();
                regenerateNames();
                setAddWordLink();
            });

            // drop downs
            word.find('.dropdown').dropdown();

            // part of speech
            bindPartOfSpeech(word);

            const setSelectedDistance = function (el) {
                const distanceType = word.find('input[type="radio"][name*="DistanceType"]:checked').val();

                // remove all selected classes
                $(el).parent().children().removeClass('selected range first last');

                // set correct classes
                const position = parseInt($(el).text());
                if (distanceType === 'Distance' && position > 1) {
                    const isLeft = $(el).closest('div').hasClass('left');

                    $(el).parent().children().each(function () {
                        const elPosition = parseInt($(this).text());
                        if (elPosition === 1) {
                            if (isLeft) {
                                $(this).addClass('selected range last');
                            } else {
                                $(this).addClass('selected range first');
                            }
                        } else if (elPosition === position) {
                            if (isLeft) {
                                $(this).addClass('selected range first');
                            } else {
                                $(this).addClass('selected range last');
                            }
                        } else if (elPosition > 1 && elPosition < position) {
                            $(this).addClass('selected range');
                        }
                    });
                } else {
                    $(el).addClass('selected');
                }

                $(el).siblings('input[type="hidden"]').val(position);
            };

            // distance type selector
            word.find('input[type="radio"][name*="DistanceType"]').change(function () {
                const distanceType = $(this).val();
                if (distanceType === 'Position') {
                    setSelectedDistance(word.find('.distance-selector .left .range.first'))
                    setSelectedDistance(word.find('.distance-selector .right .range.last'))
                } else {
                    setSelectedDistance(word.find('.distance-selector .left .selected'))
                    setSelectedDistance(word.find('.distance-selector .right .selected'))
                }
            });

            // distance selector
            word.find('.distance-selector .left span, .distance-selector .right span').click(function () {
                setSelectedDistance($(this));
            });
        };

        $('.add-word').click(function () {
            // Create clone
            const clone = $('.word-in-context.original').clone();
            clone.removeClass('original').addClass('copy');

            // Regenerate names and ids
            regenerateNames(clone);

            // Insert clone
            clone.insertAfter('.word-in-context:last');

            // Bind events
            bindEvents(clone);

            // Setup link for word in context
            setAddWordLink();

            // Display clone
            clone.show();
            return false;
        });

        // Submit
        $('form').submit(function () {
            // Save to history
            const historyItem = {
                MainWord: {
                    Form: $('#MainWord_Form').val(),
                    TranscriptionType: $('input[name="MainWord.TranscriptionType"]:checked').val(),
                    FormSearchType: $('input[name="MainWord.FormSearchType"]:checked').val(),
                    PartOfSpeechCondition: $('#MainWord_PartOfSpeechCondition').val(),
                    PartOfSpeechId: $('#MainWord_PartOfSpeechId').val()
                },
                WordsInContext: []
            };
            let cnt = 0;
            $('.word-in-context:not(.original)').each(function () {
                const wordInContext = {
                    Condition: $('input[name="WordsInContext[' + cnt + '].Condition"]:checked').val(),
                    Form: $('#WordsInContext\\[' + cnt + '\\]_Form').val(),
                    TranscriptionType: $('input[name="MainWord.TranscriptionType"]:checked').val(),
                    FormSearchType: $('input[name="WordsInContext[' + cnt + '].FormSearchType"]:checked').val(),
                    PartOfSpeechCondition: $('#WordsInContext\\[' + cnt + '\\]_PartOfSpeechCondition').val(),
                    PartOfSpeechId: $('#WordsInContext\\[' + cnt + '\\]_PartOfSpeechId').val(),
                    DistanceType: $('input[name="WordsInContext[' + cnt + '].DistanceType"]:checked').val(),
                    LeftPosition: $('#WordsInContext\\[' + cnt + '\\]_LeftPosition').val(),
                    RightPosition: $('#WordsInContext\\[' + cnt + '\\]_RightPosition').val()
                };
                historyItem.WordsInContext.push(wordInContext);
                cnt = cnt + 1;
            });

            History.saveHistoryItem('concordance-search-history', historyItem);

            // Remove original
            $('.word-in-context.original').remove();
        });
    },

    bindConcordanceSearchResults: function () {
        $('body.concordance.results .concordance-item .show-details').click(function () {
            $('body.concordance.results .concordances .concordance-item').show();
            $('body.concordance.results .concordances .details-item').hide().empty();

            const concordance = $(this).closest('.concordance-item');
            const url = $(this).data('url');
            const detailsHolder = concordance.next('.details-item');
            $(detailsHolder).load(url, function () {
                detailsHolder.show();
                concordance.hide();
                Gos.bindConcordanceSearchResultsDetails();
            });
        });
    },

    bindConcordanceSearchResultsDetails: function () {
        Gos.bindTabs();

        $('body.concordance.results .details .btn-exit').click(function () {
            const detailsHolder = $(this).closest('.hit-details');
            detailsHolder.prev('.hit-concordance').show();
            detailsHolder.hide().empty();
            return false;
        });

        $('body.concordance.results .details-item .statement-text').click(function () {
            $('body.concordance.results .details-item .statement-text').removeClass('active');
            $(this).addClass('active');

            const statementId = $(this).data('statement-id');
            $('body.concordance.results .details-item .statement-wrapper').hide();
            $('body.concordance.results .details-item .annotation-wrapper').hide();
            $('body.concordance.results .details-item .statement-wrapper[data-statement-id="' + statementId + '"]').show();
            $('body.concordance.results .details-item .annotation-wrapper[data-statement-id="' + statementId + '"]').show();
        });

        Gos.bindPlay();
    },

    bindTabs: function () {
        $('a[role="tab"]').click(function () {
            $('a[role="tab"]').parent('li').removeClass('active');
            $(this).parent('li').addClass('active');

            const target = $(this).attr('target');
            if (target) {
                $('div[role="tab-pane"]').hide();
                $(target).show();
                return false;
            }
        });
    },

    bindList: function () {
        if (!($('body').hasClass('list'))) {
            return;
        }

        $('.search-input form').submit(function () {
            const input = $(this).find('input:text');
            const query = input.val();
            const transcriptionType = $('#TranscriptionType').val();
            if (query === '') {
                input.focus();
                return false;
            }

            // Copy pos selection data into form
            $('#PartOfSpeechIds').val($('#PartOfSpeechIds_Temp').val());
            $('#Condition').val($('#Condition_Temp').val());

            // Save search to history
            const historyKey = 'list-search-history';
            const historyItem = {query: query, transcriptionType: transcriptionType};
            History.saveHistoryItem(historyKey, historyItem);

            // Remove all empty hidden inputs
            $('form input[type="hidden"]').each(function () {
                const value = $(this).val();
                if (!value) {
                    $(this).remove();
                }
            });
        });
    },

    bindListIndex: function () {
        if (!($('body').hasClass('list') && $('body').hasClass('index'))) {
            return;
        }

        // Toggle additional visibility
        const toggleAdditional = function () {
            $('.search-dropdown.pos').toggle();
            $('.search-input').toggleClass('autocomplete');
            const icon = $('#posIcon a img');
            if ($('.search-input').hasClass('autocomplete')) {
                Utils.changeImageSrc(icon, 'caret--up.svg');
            } else {
                Utils.changeImageSrc(icon, 'caret--down.svg');
            }
        };

        $('#posIcon a').click(function () {
            toggleAdditional();
        });

        $('.pos li').click(function () {
            const value = $(this).data('value');
            $('#PartOfSpeechIds').val(value);
            toggleAdditional();
        });

        $('.pos li button').click(function () {
            const partOfSpeechId = $(this).closest('li').data('value');
            Pos.showDetails(partOfSpeechId, function (values) {
                $('#Msds').val(values);
            });
        });
    },

    bindListSearchResults: function () {
        if (!($('body').hasClass('list') && $('body').hasClass('results'))) {
            return;
        }
        $('.col-group a.conversational, .col-group a.standard').click(function () {
            let conversational = '';
            let standard = '';
            const group = $(this).closest('.col-group');
            if (group.hasClass('selected')) {
                group.removeClass('selected');
            } else {
                $('.col-con-body .col-group').removeClass('selected');
                group.addClass('selected');

                conversational = group.find('.conversational').text();
                standard = group.find('.standard').text();
            }
            $('#ConversationalForm').val(conversational);
            $('#StandardForm').val(standard);
            return false;
        });

        $('.list-grouping a.grouping').click(function () {
            const selected = $(this).data('selected').toLowerCase() === 'true';
            $('#GroupByMsd').val(!selected);
            $('form.search-form').submit();
            return false;
        });
    },

    bindPlay: function () {
        $('body.concordance.results .concordance-item .play, body.concordance.results .details-item .play').click(function (e) {
            e.stopPropagation();
            const link = $(this);
            const icon = $(this).find('img');
            let state = link.data('state');

            const initialize = function () {
                // Initialize state
                const sounds = link.data('sounds');
                if (!(sounds && sounds !== '')) {
                    return;
                }

                state = {
                    files: sounds.split(','),
                    currentFile: 0,
                    audio: new Audio()
                };
                link.data('state', state);

                $(state.audio).on('playing', function () {
                    Utils.changeImageSrc(icon, 'pause--filled.svg');
                    icon.attr('pause');
                });

                $(state.audio).on('ended', function () {
                    Utils.changeImageSrc(icon, 'volume--up.svg');
                    if (state.currentFile < state.files.length - 1) {
                        state.currentFile++;
                        state.audio.src = state.files[state.currentFile];
                        state.audio.play();
                    } else {
                        state.currentFile = 0;
                        state.audio.src = state.files[state.currentFile];
                    }
                });

                $(state.audio).on('pause', function () {
                    Utils.changeImageSrc(icon, 'volume--up.svg');
                });

                // Start playing first file
                state.audio.src = state.files[state.currentFile];
                state.audio.play();
            };

            if (!state) {
                initialize();
            } else if (state.audio.paused) {
                state.audio.play();
            } else {
                state.audio.pause();
            }

            return false;
        });
    },

    bindViri: function () {
        $(document).on('click',
            '.viri-btn',
            function () {
                const q = $('.search-input input:text').val();
                if (q === '') {
                    return false;
                }

                if (!$(this).hasClass('show')) {
                    $(this).addClass('show');
                    $(this).addClass('loading-anim');

                    $("#ViriCjvt").empty().css('height', '0').css('overflow', 'scroll')
                        .css('transition', 'height 0.3s ease-out').append($('<iframe>')
                        .attr('src', 'https://viri.cjvt.si/slv/search_results_iframe_v2/' + q + '?source=gos')
                        .css('display', 'flex')
                        .css('width', '100%')
                    );
                } else {
                    $(this).removeClass('show');
                    $("#ViriCjvt").css('transition', 'height 0.3s ease-out').css('height', '0');
                }
            });

        const resizeIframe = function (event) {
            $("#searchViriCjvt").removeClass('loading-anim');
            const $iframe = $("#ViriCjvt,#ViriCjvt iframe").css('height', event.data + "px");
        };
        if (window.addEventListener) {
            window.addEventListener("message", resizeIframe, false);
        } else if (window.attachEvent) {
            window.attachEvent("onmessage", resizeIframe);
        }
    }
};

$(Gos.init);
