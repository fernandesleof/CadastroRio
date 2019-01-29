jQuery(function($) {

    var multiTags = $("#multi");
    var conttxt = 1 ;

    function handler(e) {
        var jqEl = $(e.currentTarget);
        var tag = jqEl.parent();
        switch (jqEl.attr("data-action")) {

        case "add":
            conttxt++;
            tag.after(tag.clone().find("select").val("").end());
            break;   
        case "delete":
            
            if(conttxt>1){
                conttxt--;
                tag.remove();
            }
            break;         
        }
        return false;
    }
    
    multiTags.find("a").live("click", handler);
});