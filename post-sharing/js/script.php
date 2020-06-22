
/**
 * Created by PhpStorm.
 * User: danieln
 * Date: 6/16/2017
 * Time: 4:48 PM
 */

<script>
    var ui=JSON.parse('<?php get_option('mss-facebook-page-list'); ?>');
    var input=document.getElementsByName('mss-facebook-page-list[][id]');

    for(var i=0;i<input.length;i++)
    {
        for(var j=0;j<ui.length;j++)
        {
            if(ui[j]["id"]==input[i].getAttribute("value"))
            {
                input[i].setAttribute("checked","checked");
                break;
            }
        }
    }

</script>