<?php
/**
 * Plugin Name: Post Sharing
 * Plugin URI:  https://github.com/danielmobiletech/projects
 * Description: Automatically shares published post posts
 * Author:      Daniel Hall
 * Author URI:  https://github.com/danielmobiletech/projects
 * Version:     1.0.0
 * Text Domain: Post Sharing
 * Domain Path: /languages/
 */


require_once __DIR__.'/php-graph-sdk-5.x/src/Facebook/autoload.php';




// create custom plugin settings menu
add_action('admin_menu', 'my_cool_plugin_create_menu');

function my_cool_plugin_create_menu() {

    //create new top-level menu
    add_menu_page('Post Sharing', 'Post Sharing', 'administrator',"post_shares", 'my_cool_plugin_settings_page' , plugins_url('/images/icon.png', __FILE__) );

    //call register settings function
    add_action( 'admin_init', 'register_my_cool_plugin_settings' );
}

function category_filter($input)
{
    $output=[];
    if(is_array($input))
    {
        for($i=0;$i<count($input);$i++) {
            if(preg_match('/^\d+$/',$input[$i]))
            {
                $output[] = $input[$i];
            }
        }
        return implode(',',$output);
    }
    return '';

}
 function facebook_page_list_filter_array($input)
 {
     $output=[];
     if(is_array($input))
     {
         for($i=0;$i<count($input);$i++) {
             if(preg_match('/^\d+$/',$input[$i]['id']))
             {
                 $output[]['id'] = $input[$i]['id'];
             }
         }
          return json_encode($output);
     }
     return '';
 }
function register_my_cool_plugin_settings() {



    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_enable' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_app_id' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_app_secret' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_app_name' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_page_list' );


    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_page_enable_update' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_page_enable' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_page_format_message' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_page_message' );


    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_enable' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_app_id' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_app_secret' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_page_list','facebook_page_list_filter_array' );


    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_enable' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_format_message' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_message' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_categories','category_filter' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_enable_update' );

    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_wc_enable' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_format_wc_message' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_wc_message' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_wc_categories','category_filter' );
    register_setting( 'my_cool_plugin_settings_group', 'mss_facebook_auto_wc_enable_update' );

 
}



add_action('init', 'myStartSession', 1);
add_action('wp_logout', 'myEndSession');
add_action('wp_login', 'myEndSession');

function myStartSession() {
    if(!session_id()) {
        session_start();
    }
}

function myEndSession() {
    session_destroy ();
}



function my_cool_plugin_settings_page() {
   // session_start();
    $fbPermission=array('email'/*, 'publish_pages','manage_pages','publish_pages'*/);

    if( get_option('mss_facebook_app_id')!=='' && get_option('mss_facebook_app_secret')!=='') {
        $fb = new Facebook\Facebook(['app_id' => get_option('mss_facebook_app_id'), 'app_secret' => get_option('mss_facebook_app_secret'), 'default_graph_version' => 'v3.0']);
        // $fb=new Facebook\Facebook(['app_id'=>appId,'app_secret'=>appSecret,'default_graph_version'=>'v2.9']);
        $helper = $fb->getRedirectLoginHelper();
	    if (isset($_GET['state'])) {
		    $helper->getPersistentDataHandler()->set('state', $_GET['state']);
	    }
        //$fbloginurl='';
        if (get_option('mss_facebook_access_token') == '') {
            
            try {
	            $accesstoken = $helper->getAccessToken();

	            
            }
            catch(\Facebook\Exceptions\FacebookSDKException $e)
            {
                echo "thhy reads error code  ";
                echo $e->getMessage();
                echo " code displlayed";
            }
             echo '<br/>'.$accesstoken.'<br/>';
            if (isset($accesstoken)) {
                $fb->setDefaultAccessToken($accesstoken);
                $toks=$fb->get('/debug_token?input_token='.$accesstoken);
                

                $oauth = $fb->getOAuth2Client();
                $longlivetoken = $oauth->getLongLivedAccessToken($accesstoken);
                update_option('mss_facebook_access_token', (string)$longlivetoken);
                //echo '<br/>'.get_option('mss_facebook_access_token').'<br/>';
            }


        }


        $fbloginurl = $helper->getLoginUrl(menu_page_url('post_shares', false), $fbPermission);
    }

    

    $args = array( 'numberposts' => '1' );
    $recent_posts = wp_get_recent_posts( $args );
    $post_data=array();
    foreach( $recent_posts as $recent ){
        $post_recent_ids[]=$recent["ID"];
        $post_data[]=$recent['post_title'];
    }
    wp_reset_query();

    $checkedlists=json_decode(get_option('mss_facebook_page_list'),true);
    $cat_match=false;

    // $defaults = array('fields' => 'ids');
    // $carr2=wp_get_post_categories( $recent_posts, $defaults );



    $post_categories = wp_get_post_categories(  $post_recent_ids[0] );
    

    



    

    foreach ( $post_categories as $catz)
    {
       if(in_array($catz, $chosencat))
       {
           $cat_match=true;
           break;
       }
    }
    
    function get_the_categories( $parent = 0 )
    {
       // $recent_posts = wp_get_recent_posts( $args );
        $defaults = array('fields' => 'ids');
        $carr2=wp_get_post_categories( 91, $defaults );
        
        $categories = get_categories( "hide_empty=0&parent=$parent&order=DESC" );

        if ( $categories ) {
            echo '<ul class="span-5 colborder list_main">';
            foreach ( $categories as $cat ) {
                if ( $cat->category_parent == $parent ) {
                    echo '<a href="' . get_category_link( $cat->term_id ) . '" >' . $cat->name . '</a>'.$cat->cat_ID.'<br/>';
                    get_the_categories( $cat->term_id );
                }
            }
            echo '</ul>';
        }
    }

    get_the_categories();



    ?>
    <div class="wrap">
        <h1>Post Sharing Facebook</h1>
        <?php if(get_option('mss_facebook_app_id')!=''&&get_option('mss_facebook_app_secret')!=''):?>
        <h2><a href="<?php echo $helper->getLoginUrl(menu_page_url('post_shares',false),$fbPermission); ?>">Facebook</a></h2>
        <?php else:?>
        <h2>Requires App ID and App Secret</h2>

        <?php endif ?>
        <form method="post" action="options.php">
            <?php wp_nonce_field( 'fb_auth_nonce' );?>
            <?php settings_fields( 'my_cool_plugin_settings_group' ); ?>
            <?php do_settings_sections( 'my_cool_plugin_settings_group' ); ?>
            <table class="form-table">
                <tr valign="top">
                    <th scope="row">App ID</th>
                    <td><input type="text" name="mss_facebook_app_id" value="<?php echo esc_attr( get_option('mss_facebook_app_id') ); ?>" /></td>
                </tr>

                <?php
                if(get_option('mss_facebook_access_token')!='')
                {
                    //Gets AppID and Secret
                    //Displays Pages that are able to be posted
                    $fb=new Facebook\Facebook(['app_id'=>get_option('mss_facebook_app_id'),'app_secret'=>get_option('mss_facebook_app_secret'),'default_graph_version'=>'v2.9']);

                    $fb->setDefaultAccessToken(get_option('mss_facebook_access_token'));
                    $profile_request = $fb->get('/me/accounts');

                    $profile = $profile_request->getGraphEdge()->asArray();
                    $htmls='';
                    if(get_option('mss_facebook_page_list')!='')
                    {


                        $checkedlists = json_decode(get_option('mss_facebook_page_list'), true);


                        $passedthru=[];
                        for($i=0;$i<count($checkedlists);$i++)
                        {
                            $passedthru[]=false;
                        }



                        for($i=0;$i<count($profile);$i++ )
                        {
                            for(  $j=0;$j<count($checkedlists);$j++)
                            {
                                if($profile[$i]['id']==$checkedlists[$j]['id']/*&&$passedthru[$j]==false*/)
                                {
                                    $passedthru[$i]=true;

                                }






                            }

                        }

                        for($i=0;$i<count($profile);$i++)
                        {
                            if($passedthru[$i]==true)
                            {
                                $htmls.='<input type="checkbox" name="mss_facebook_page_list[][id]" value="'.$profile[$i]['id'].'" checked/>'.$profile[$i]['name'].'<br/>';
                            }
                            else
                            {
                                $htmls.='<input type="checkbox" name="mss_facebook_page_list[][id]" value="'.$profile[$i]['id'].'" />'.$profile[$i]['name'].'<br/>';

                            }
                        }
                        echo $htmls;

                        }

                    else{


                        for($i=0;$i<count($profile);$i++)
                        {
                            $htmls.='<input type="checkbox" name="mss_facebook_page_list[][id]" value="'.$profile[$i]['id'].'" />'.$profile[$i]['name'].'<br/>';

                        }
                        echo $htmls;
                    }








                }



                function genereatedropdownwc ()
                {
                    $args = array(
                        'show_option_all' => '',
                        'show_option_none' => '',
                        'orderby' => 'name',
                        'order' => 'ASC',
                        'value_field' => 'term_id',
                        'show_last_update' => 0,
                        'show_count' => 0,
                        'hide_empty' => 0,
                        'child_of' => 0,
                        'exclude' => '',
                        'echo' => 0,
                        'selected' => '24',
                        'hierarchical' => 1,
                        'id' => 'fbap_catlist',
                        'class' => 'postform',
                        'depth' => 0,
                        'tab_index' => 0,
                        'taxonomy' => 'product_cat',
                        'hide_if_empty' => false);


                    if (count(get_categories($args)) > 0) {

                        $categories='';
                        if(get_option('mss_facebook_auto_categories')!='') {
                            $categories = explode(',', get_option('mss_facebook_auto_wc_categories'));
                        }
                        $filt_pass = str_replace("<select", "<select multiple onClick=setcat(this) style='width:200px;height:auto !important;border:1px solid #cccccc;' name='mss_facebook_auto_wc_categories[]'", wp_dropdown_categories($args));
                        if(is_array($categories)) {
                            foreach ($categories as $category) {
                                $filt_pass = str_replace('value="' . $category . '"', 'value="' . $category . '" selected=selected', $filt_pass);
                            }
                        }


                        echo $filt_pass;
                        //echo str_replace( "<select", "<select multiple onClick=setcat(this) style='width:200px;height:auto !important;border:1px solid #cccccc;'", wc_product_dropdown_categories($args));

                    } else
                        echo "NIL";
                    //mss_facebook_page_enable


                }


                function genereatedropdown()
                {

                                        $args = array(
                                            'show_option_all'    => '',
                                            'show_option_none'   => '',
                                            'orderby'            => 'name',
                                            'order'              => 'ASC',
                                            'show_last_update'   => 0,
                                            'show_count'         => 0,
                                            'hide_empty'         => 0,
                                            'child_of'           => 0,
                                            'exclude'            => '',
                                            'echo'               => 0,
                                            'selected'           => '1 3',
                                            'hierarchical'       => 1,
                                            'id'                 => 'fbap_catlist',
                                            'class'              => 'postform',
                                            'depth'              => 0,
                                            'tab_index'          => 0,
                                            'taxonomy'           => 'category',
                                            'hide_if_empty'      => false );

                    if (count(get_categories($args)) > 0) {

                        $categories='';
                        if(get_option('mss_facebook_auto_categories')!='') {
                            $categories = explode(',', get_option('mss_facebook_auto_categories'));
                        }
                        
                        $filt_pass = str_replace("<select", "<select multiple onClick=setcat(this) style='width:200px;height:auto !important;border:1px solid #cccccc;' name='mss_facebook_auto_categories[]'", wp_dropdown_categories($args));

                        if(is_array($categories))
                        {
                            foreach ($categories as $category) {
                                $filt_pass = str_replace('value="' . $category . '"', 'value="' . $category . '" selected=selected', $filt_pass);
                            }
                        }

                        echo $filt_pass;
                        //echo str_replace( "<select", "<select multiple onClick=setcat(this) style='width:200px;height:auto !important;border:1px solid #cccccc;'", wc_product_dropdown_categories($args));

                    } else
                        echo "NIL";
                    //mss_facebook_page_enable


                }

                function enabler($opt)
                {
                    $data='<option value="0">Disabled</option><option value="1">Enable</option>';
                    $val=get_option($opt);

                    if( $val==0)
                    {
                        $data=str_replace('value="0"',"value='{$val}' selected=selected",$data);
                        return $data;
                    }



                    $data=str_replace('value="1"',"value='{$val}' selected=selected",$data);
                    return $data;
                }





                ?>

                <tr valign="top">
                    <th scope="row">App Secret</th>
                    <td><input type="text" name="mss_facebook_app_secret" value="<?php echo esc_attr( get_option('mss_facebook_app_secret') ); ?>" /></td>
                </tr>

                <tr valign="top">
                    <th scope="row">Facebook Enable</th>
                    <td><select name="mss_facebook_enable"><?php echo enabler('mss_facebook_enable'); ?></select></td>
                </tr>

                 <tr valign="top">
                    <th scope="row">App Name</th>
                    <td><input type="text" name="mss_facebook_app_name" value="<?php echo esc_attr( get_option('mss_facebook_app_name') ); ?>" /></td>
                </tr>
               <!-- <tr valign="top">
                    <th scope="row">App Name</th>
                    <td><input type="text" name="mss_facebook_app_name" value="<?php /*echo esc_attr( get_option('mss_facebook_app_name') ); */?>" /></td>
                </tr>-->

            </table>

            <br/>
            <br/>
            <table class="form-table">

                <span id="cat_dropdown_span"><br /> <br /> <?php


                    ?>
                    <br/>
                    <br/>

                    <tr valign="top">
                        <th scope="row">App Page Enable</th>
                        <td><select name="mss_facebook_page_enable"><?php echo enabler('mss_facebook_page_enable'); ?></select></td>
                    </tr>

                    <tr valign="top">
                        <th scope="row">App Page Enable Update</th>
                        <td><select name="mss_facebook_page_enable_update"><?php echo enabler('mss_facebook_page_enable_update'); ?></select></td>
                    </tr>
                    <!--<tr valign="top">
                        <th scope="row">App Post Allowed Categories</th>
                        <td><?php echo  genereatedropdown(); ?></td>
                    </tr>-->
                    <tr valign="top">
                        <th scope="row">App Page Post Message</th>
                        <td><input type="text" name="mss_facebook_page_message" value="<?php echo esc_attr( get_option('mss_facebook_page_message') ); ?>" /></td>
                    </tr>

                    
                    <br/>
                    <br/>







            </table>














            <table class="form-table">
                <tr valign="top">
                    <th scope="row">Post Enable</th>
                    <td><select name="mss_facebook_auto_enable"><?php echo enabler('mss_facebook_auto_enable');  ?></select></td>
                </tr>
                <tr valign="top">
                    <th scope="row">App Post Enable Update</th>
                    <td><select name="mss_facebook_auto_enable_update"><?php echo enabler('mss_facebook_auto_enable_update');  ?></select></td>
                </tr>
                <tr valign="top">
                    <th scope="row">App Post Allowed Categories</th>
                    <td><?php echo  genereatedropdown(); ?></td>
                </tr>
                <tr valign="top">
                    <th scope="row">App Post Message Format</th>
                    <td>
                        <select>
                            <option value="[POST_TITLE]">[POST_TITLE]</option>
                            <option value="[POST_EXCERPT]">[POST_EXCERPT]</option>
                            <option value="[POST_AUTHOR]">[POST_AUTHOR]</option>
                            <option value="[POST_LINK]">[POST_LINK]</option>
                            <option value="[POST_DESCRIPTION]">[POST_DESCRIPTION]</option>
                            <option value="[BLOG_TITLE]">[BLOG_TITLE]</option>
                        </select>
                    </td>
                </tr>
                <tr valign="top">
                    <th scope="row">App Post Message</th>
                    <td><input type="text" name="mss_facebook_auto_message" value="<?php echo esc_attr( get_option('mss_facebook_auto_message') ); ?>" /></td>
                </tr>


            </table>


            <br/>
            <br/>


            <table class="form-table">
                <tr valign="top">
                    <th scope="row">Woocommerce Post Enable</th>
                    <td><select name="mss_facebook_auto_wc_enable"><?php echo enabler('mss_facebook_auto_wc_enable');  ?></select></td>
                </tr>
                <tr valign="top">
                    <th scope="row">App Woocommerce Post Enable Update</th>
                    <td><select name="mss_facebook_auto_wc_enable_update"><?php echo enabler('mss_facebook_auto_wc_enable_update');  ?></select></td>
                </tr>
                <tr valign="top">
                    <th scope="row">Woocommerce App Post Allowed Categories</th>
                    <td><?php echo  genereatedropdownwc(); ?></td>
                </tr>
                <tr valign="top">
                    <th scope="row">Woocommerce App Post Message Format</th>
                    <td>
                        <select>
                            <option value="[WC_PRODUCT_NAME]">[WC_PRODUCT_NAME]</option>
                            <option value="[WC_PRODUCT_PRICE]">[WC_PRODUCT_PRICE]</option>
                            <option value="[WC_PRODUCT_DESCRIPTION]">[WC_PRODUCT_DESCRIPTION]</option>
                            <option value="[WC_PRODUCT_SHORT_DESCRIPTION]">[WC_PRODUCT_SHORT_DESCRIPTION]</option>
                            <option value="[WC_PRODUCT_LINK]">[WC_PRODUCT_LINK]</option>
                            <option value="[BLOG_TITLE]">[BLOG_TITLE]</option>
                        </select>
                    </td>
                </tr>
                <tr valign="top">
                    <th scope="row">Woocommerce App Post Message</th>
                    <td><input type="text" name="mss_facebook_auto_wc_message" value="<?php echo esc_attr( get_option('mss_facebook_auto_wc_message') ); ?>" /></td>
                </tr>



            </table>

            <?php submit_button(); ?>

        </form>
    </div>
<?php } ?>




<?php












function textx()
{

    $fops=fopen(__DIR__.'/filops/savqm.txt','x+');

    //$idles = get_wp_rece$args = array( 'numberposts' => '5' );
    $args = array( 'numberposts' => '1' );
    $post_recent_ids=array();
    $recent_posts = wp_get_recent_posts( $args );
    $post_data=array();
    foreach( $recent_posts as $recent ){
        // echo '<li><a href="' . get_permalink($recent["ID"]) . '">' .   $recent["post_title"].'</a> </li> ';
        $post_recent_ids[]=$recent["ID"];
        $post_data[]=$recent['post_title'];
    }
    wp_reset_query();

    fwrite($fops,'go home now '.$post_recent_ids[0].menu_page_url(__FILE__) .$post_data[0].get_permalink($post_recent_ids[0]));
    fclose($fops);


}


function add_error_list(&$error_logs,$post_id,$post_title,$post_date,$error_code,$error_message)
{
   // $error_logs=get_option('mss_facebook_error_logs');

    if(!is_array($error_logs)&&$error_logs=='')
    {
        $error_logs=[];

    }
    else{
        $error_logs=json_decode( $error_logs,true);
    }
    if(is_array($error_logs))
    {
        if (count($error_logs) > 9)
        {

            while (count($error_logs) > 9) {



                for ($i = 0; $i < count($error_logs) - 1; $i++) {
                    $error_logs[$i] = $error_logs[$i + 1];
                }
                array_pop($error_logs);
            }

        }
        $error_logs[]= ['post_id' => $post_id, 'post_title' => $post_title, 'post_date' => $post_date, 'error_code' => $error_code, 'error_message' => $error_message];
        // array_push($error_logs, $rr);

    }
    $errors=json_encode($error_logs);
    update_option('mss_facebook_error_logs',$errors);
}

function update_mss_social_media_options()
{
    //Facebook App Settings


    update_option('mss_facebook_enable','');
    update_option('mss_facebook_access_token','');
    update_option('mss_facebook_app_id','594100954281443');
    update_option('mss_facebook_app_secret','85b1bc4313ed414ab82ce8fb7d5148a7');
    update_option('mss_facebook_app_name','');
    update_option('mss_facebook_page_list','[ { "id": "1040518752675238" }, { "id": "571246859688423" }, { "id": "1474270522811196" }, { "id": "134897436625237" } ]');




    //Page Settings
    update_option('mss_facebook_page_message','');
    update_option('mss_facebook_page_format_message','');
    update_option('mss_facebook_update_enable','1');
    update_option('mss_facebook_page_enable','0');
    update_option('mss_facebook_epage_error_logs','');


    //Posting Setting
    update_option('mss_facebook_auto_enable','');
    update_option('mss_facebook_auto_format_message','');
    update_option('mss_facebook_auto_message','');
    update_option('mss_facebook_auto_image','');
    update_option('mss_facebook_auto_categories','');
    update_option('mss_facebook_auto_enable_update','0');
    update_option('mss_facebook_error_logs','');




    //Wocommerce Settings
    update_option('mss_facebook_auto_wc_enable','');
    update_option('mss_facebook_auto_wc_format_message','');
    update_option('mss_facebook_auto_wc_message','');
    update_option('mss_facebook_auto_wc_categories','');
    update_option('mss_facebook_auto_wc_image','');
    update_option('mss_facebook_auto_wc_enable_update','0');
    update_option('mss_facebook_wc_error_logs','');




}

function add_mss_social_media_options()
{

//Facebook App Settings

    add_option('mss_facebook_enable','');
    add_option('mss_facebook_access_token','');
    add_option('mss_facebook_app_id','');
    add_option('mss_facebook_app_secret','');
    add_option('mss_facebook_app_name','');
    add_option('mss_facebook_page_list','');




    //Page Settings
    add_option('mss_facebook_page_message');
    add_option('mss_facebook_page_format_message','');
    add_option('mss_facebook_update_enable','0');
    add_option('mss_facebook_page_enable','0');
    add_option('mss_facebook_page_error_logs');


    //Posting Setting
    add_option('mss_facebook_auto_enable','');
    add_option('mss_facebook_auto_format_message','');
    add_option('mss_facebook_auto_message','');
    add_option('mss_facebook_auto_image','');
    add_option('mss_facebook_auto_categories','');
    add_option('mss_facebook_auto_enable_update','0');
    add_option('mss_facebook_error_logs','');



    //Wocommerce Settings
    add_option('mss_facebook_auto_wc_enable','');
    add_option('mss_facebook_auto_wc_format_message','');
    add_option('mss_facebook_auto_wc_message','');
    add_option('mss_facebook_auto_wc_categories','');
    add_option('mss_facebook_auto_wc_image','');
    add_option('mss_facebook_auto_wc_enable_update','0');
    add_option('mss_facebook_wc_error_logs','');


}


function delete_mss_social_media_options()
{

    //Facebook App Settings

    delete_option('mss_facebook_enable');
    delete_option('mss_facebook_access_token');
    delete_option('mss_facebook_app_id');
    delete_option('mss_facebook_app_secret');
    delete_option('mss_facebook_app_name');
    delete_option('mss_facebook_page_list');



    //Page Settings
    delete_option('mss_facebook_page_format_message');
    delete_option('mss_facebook_page_message');
    delete_option('mss_facebook_update_enable');
    delete_option('mss_facebook_page_enable');
    delete_option('mss_facebook_error_logs');


    //Posting Setting
    delete_option('mss_facebook_auto_enable');
    delete_option('mss_facebook_auto_format_message');
    delete_option('mss_facebook_auto_message');
    delete_option('mss_facebook_auto_image');
    delete_option('mss_facebook_auto_categories');
    delete_option('mss_facebook_auto_enable_update');
    delete_option('mss_facebook_error_logs');



    //Wocommerce Settings
    delete_option('mss_facebook_auto_wc_enable');
    delete_option('mss_facebook_auto_wc_format_message');
    delete_option('mss_facebook_auto_wc_message');
    delete_option('mss_facebook_auto_wc_categories');
    delete_option('mss_facebook_auto_wc_image');
    delete_option('mss_facebook_auto_wc_enable_update');
    delete_option('mss_facebook_wc_error_logs');



}
register_activation_hook( __FILE__, 'add_mss_social_media_options'   );
register_deactivation_hook( __FILE__, 'update_mss_social_media_options' );
register_uninstall_hook(__FILE__,'delete_mss_social_media_options');
//add_action( 'deactivate_' . $this->basename, 'bp_deactivation' );















function get_latest_posts($new_status,$old_status,$post)
{
    if('publish' === $new_status && 'publish' !== $old_status && $post->post_type === 'product')
    {
        $rfz=wc_get_product($post->ID);

        $message=get_option('mss_facebook_auto_wc_message');
        message_wc_sales_parser($message,$post);
       // header('Location: http://www.example.com/');
       
        // do stuff
    }


    elseif('publish' === $new_status && 'publish' !== $old_status && $post->post_type === 'post') {

        if (get_option('mss_facebook_enable') == 1 && get_option('mss_facebook_auto_enable') == 1) {
            if (get_option('mss_facebook_access_token') != '') {
                if (get_option('mss_facebook_page_list') != '') {
                    $message=get_option(mss_facebook_auto_message);
                    message_post_parser($message,$post);
                    $args = array('numberposts' => '1');

                    /*$recent_posts = wp_get_recent_posts( $args );
                    $post_data=array();
                    foreach( $recent_posts as $recent ){
                        // echo '<li><a href="' . get_permalink($recent["ID"]) . '">' .   $recent["post_title"].'</a> </li> ';
                        $post_recent_ids[]=$recent["ID"];
                        $post_data[]=$recent['post_title'];
                    }
                    wp_reset_query();*/
                    $checkedlists = json_decode(get_option('mss_facebook_page_list'), true);
                    $dcatmatch = 0;
                    $chosencat = explode(',', get_option('mss_facebook_auto_categories'));

                    $post_categories = wp_get_post_categories($post->ID);


                    foreach ($post_categories as $catz) {

                        if (in_array((string)$catz, $chosencat)) {
                           // $fops = fopen(__DIR__ . '/filops/' . $catz . ' jh   ' . $post->post_title . 'kkiim.txt', 'x+');

                            //$idles = get_posts("post_type=yourcpt&numberposts=1&fields=ids");

                           // fwrite($fops, ' go home now ' . $catz . '   ' . $post->post_author);
                           // fclose($fops);

                            $dcatmatch = 1;
                            //break;
                        }

                    }

                    try {
                        $fb = new Facebook\Facebook(['app_id' => get_option('mss_facebook_app_id'), 'app_secret' => get_option('mss_facebook_app_secret'), 'default_graph_version' => 'v2.9']);
                        $fb->setDefaultAccessToken(get_option('mss_facebook_access_token'));

                        $profile_request = $fb->get('/me/accounts');

                        $profile = $profile_request->getGraphEdge()->asArray();
                        // $pages=array();
                                               if ($dcatmatch == 1) {
                            foreach ($profile as $profs) {
                                foreach ($checkedlists as $checkedlist) {

                                    if ($profs['id'] == $checkedlist['id']) {
                                        //$carr1=explode(',', $fbap_include_categories);


                                        $fb->post('/' . $profs['id'] . '/feed', ['message' => $message/*$post->post_title . get_the_author_meta('display_name', $post->post_author)]*/], $profs['access_token']);
                                        //break;
                                    }
                                }

                            }
                        }



                }
                    catch(\Facebook\Exceptions\FacebookSDKException $e)
                    {
                        //add_error_list(&$error_logs,$post_id,$post_title,$post_date,$error_code,$error_message)
                        $error_logs=get_option('mss_facebook_error_logs');

                        add_error_list($error_logs,$post->ID,$post->post_title,get_the_date('Y/n/d h:i a T ',$post->ID),$e->getCode(),$e->getMessage());
                       // $fops = fopen(__DIR__ . '/filops/' . $catz . ' sdk error ' . $post->post_title . 'kkiim.txt', 'x+');

                        //$idles = get_posts("post_type=yourcpt&numberposts=1&fields=ids");

                        //fwrite($fops, ' go home now ' . $catz . $e->getMessage() . $post->post_author);
                        //fclose($fops);
                    }
                    catch(\Facebook\Exceptions\FacebookResponseException $e)
                    {
                        $fops = fopen(__DIR__ . '/filops/' . $catz . ' response error ' . $post->post_title . 'kkiim.txt', 'x+');

                        //$idles = get_posts("post_type=yourcpt&numberposts=1&fields=ids");

                        fwrite($fops, ' go home now ' . $catz . $e->getMessage() . $post->post_author);
                        fclose($fops);
                    }
                }

            }
        }
    }





}

function message_post_parser(string &$message,WP_Post $post)
{
    $message= str_replace('[POST_TITLE]',$post->post_title,$message);
    $message= str_replace('[POST_EXCERPT]',get_the_excerpt($post->ID),$message);
    $message= str_replace('[POST_AUTHOR]',get_the_author_meta('display_name', $post->post_author),$message);
    $message= str_replace('[POST_LINK]',esc_url(get_post_permalink($post->ID)),$message);
    $message= str_replace('[POST_DESCRIPTION]',esc_url(get_post_permalink($post->post_content)),$message);
    $message= str_replace('[BLOG_TITLE]',get_blog_details(get_current_blog_id())->blogname,$message);
    $wp_select_options=' <select>
                <option value="[POST_TITLE]">[POST_TITLE]</option>
                <option value="[POST_EXCERPT]">[POST_EXCERPT]</option>
                <option value="[POST_AUTHOR]">[POST_AUTHOR]</option>
                <option value="[POST_LINK]">[POST_LINK]</option>
                <option value="[BLOG_TITLE]">[BLOG_TITLE]</option>
            </select>';




}

function message_wc_parser(string &$message,WP_Post $post)
{
    $message= str_replace('[WC_PRODUCT_NAME]',wc_get_product($post->ID)->get_name(),$message);
    $message= str_replace('[WC_PRODUCT_PRICE]',get_woocommerce_currency_symbol().wc_get_product($post->ID)->get_price(),$message);
    $message= str_replace('[WC_PRODUCT_DESCRIPTION]',wc_get_product($post->ID)->get_description(),$message);
    $message= str_replace('[WC_PRODUCT_LINK]',esc_url(get_post_permalink($post->ID)),$message);
    $message= str_replace('[BLOG_TITLE]',get_blog_details(get_current_blog_id())->get_blogname(),$message);


    $wp_select_options='<select>
            <option value="[WC_PRODUCT_NAME]">[WC_PRODUCT_NAME]</option>
            <option value="[WC_PRODUCT_PRICE]">[WC_PRODUCT_PRICE]</option>
            <option value="[WC_PRODUCT_DESCRIPTION]">[WC_PRODUCT_DESCRIPTION]</option>
            <option value="[WC_PRODUCT_SHORT_DESCRIPTION]">[WC_PRODUCT_SHORT_DESCRIPTION]</option>
            <option value="[WC_PRODUCT_LINK]">[WC_PRODUCT_LINK]</option>
            <option value="[BLOG_TITLE]">[BLOG_TITLE]</option>
            </select>';


}

function message_wc_sales_parser(string &$message,WP_Post $post)
{
    $message= str_replace('[WC_PRODUCT_NAME]',wc_get_product($post->ID)->get_name(),$message);
    $message= str_replace('[WC_PRODUCT_PRICE]',get_woocommerce_currency_symbol().wc_get_product($post->ID)->get_price(),$message);
    $message= str_replace('[WC_PRODUCT_DESCRIPTION]',wc_get_product($post->ID)->get_description(),$message);
    $message= str_replace('[WC_PRODUCT_LINK]',esc_url(get_post_permalink($post->ID)),$message);
    $message= str_replace('[WC_PRODUCT_SALES_PRICE]',get_woocommerce_currency_symbol().wc_get_product($post->ID)->get_sale_price(),$message);
    $message= str_replace('[WC_PRODUCT_SALES_START_DATE]',wc_format_datetime(wc_get_product($post->ID)->get_date_on_sale_from(),'M d y'),$message);
    $message= str_replace('[WC_PRODUCT_SALES_END_DATE]',wc_format_datetime(wc_get_product($post->ID)->get_date_on_sale_to(),'M d y'),$message);
    $message= str_replace('[WC_PRODUCT_SAVING_PERCENTAGE]',(1-((wc_get_product($post->ID)->get_sale_price()/wc_get_product($post->ID)->get_price())*100)).'%',$message);

    $message= str_replace('[BLOG_TITLE]',get_blog_details(get_current_blog_id())->get_blogname(),$message);

    $wp_select_options='<select>
            <option value="[WC_PRODUCT_NAME]">[WC_PRODUCT_NAME]</option>
            <option value="[WC_PRODUCT_PRICE]">[WC_PRODUCT_PRICE]</option>
            <option value="[WC_PRODUCT_DESCRIPTION]">[WC_PRODUCT_DESCRIPTION]</option>
            <option value="[WC_PRODUCT_SHORT_DESCRIPTION]">[WC_PRODUCT_SHORT_DESCRIPTION]</option>
            <option value="[WC_PRODUCT_SALES_PRICE]">[WC_PRODUCT_SALES_PRICE]</option>
            <option value="[WC_PRODUCT_SALES_START_DATE]">[WC_PRODUCT_SALES_START_DATE]</option>
            <option value="[WC_PRODUCT_SALES_END_DATE]">[WC_PRODUCT_SALES_END_DATE]</option>
            <option value="[WC_PRODUCT_SAVING_PERCENTAGE]">[WC_PRODUCT_SAVING_PERCENTAGE]</option>
            <option value="[WC_PRODUCT_LINK]">[WC_PRODUCT_LINK]</option>
            <option value="[BLOG_TITLE]">[BLOG_TITLE]</option>
            </select>';



}


function post_pushers($post)
{


    if( $post->post_type === 'post') {
       // header('Location: http://www.example.com/');
        $fops = fopen(__DIR__.'mm.txt', 'w');

        //$idles = get_posts("post_type=yourcpt&numberposts=1&fields=ids");

        fwrite($fops, 'go home now');
        fclose($fops);


    }

}
/*$attachment = array('message' => $message5,
    'access_token' => $acceses_token,
    'link' => $link,
    'name' => $name,
    'caption' => $caption,
    'description' => $description_li,
    'picture' => $attachmenturl
);*/

function updatepost($post_ID, $post_after, $post_before)
{
    if($post_before->post_status === 'publish' &&($post_before->post_title!=$post_after->post_title)) {

        if (get_option('mss_facebook_enable') == 1 && get_option('mss_facebook_auto_enable_update') == 1) {
            if (get_option('mss_facebook_access_token') != '') {
                if (get_option('mss_facebook_page_list') != '') {

                    $fb = new Facebook\Facebook(['app_id' => get_option('mss_facebook_app_id'), 'app_secret' => get_option('mss_facebook_app_secret'), 'default_graph_version' => 'v3.0']);
                    $fb->setDefaultAccessToken(get_option('mss_facebook_access_token'));

                    $profile_request = $fb->get('/me/accounts');

                    $profile = $profile_request->getGraphEdge()->asArray();
                    // $pages=array();
                    $checkedlists = json_decode(get_option('mss_facebook_page_list'), true);
                    $dcatmatch = 0;
                    $chosencat = explode(',',get_option('mss_facebook_auto_categories'));


                    $post_categories = wp_get_post_categories($post_after->ID);

                    foreach ($post_categories as $catz) {
                        if (in_array($catz, $chosencat)) {
                            $dcatmatch = 1;
                            //break;
                        }

                    }
                    if ($dcatmatch == 1) {
                        foreach ($profile as $profs) {
                            foreach ($checkedlists as $checkedlist) {

                                if ($profs['id'] == $checkedlist['id']) {
                                    //$carr1=explode(',', $fbap_include_categories);


                                    $fb->post('/' . $profs['id'] . '/feed', ['message' => $post_after->post_title.'  final test  ' . get_the_author_meta('display_name', $post_after->post_author)], $profs['access_token']);
                                    //break;
                                }
                            }

                        }
                    }

                }

            }
        }
    }

}



function wc_updatepost($post_ID, $post_after, $post_before)
{
    if($post_before->post_status === 'publish' &&($post_before->post_type==='product')) {

        if (get_option('mss_facebook_enable') == 1 && get_option('mss_facebook_auto_enable_update') == 1) {
            if (get_option('mss_facebook_access_token') != '') {
                if (get_option('mss_facebook_page_list') != '') {

                    $fb = new Facebook\Facebook(['app_id' => get_option('mss_facebook_app_id'), 'app_secret' => get_option('mss_facebook_app_secret'), 'default_graph_version' => 'v3.0']);
                    $fb->setDefaultAccessToken(get_option('mss_facebook_access_token'));

                    $profile_request = $fb->get('/me/accounts');

                    $profile = $profile_request->getGraphEdge()->asArray();
                    // $pages=array();
                    $checkedlists = json_decode(get_option('mss_facebook_page_list'), true);
                    $dcatmatch = 0;
                    $chosencat = explode(',',get_option('mss_facebook_auto_categories'));


                    $post_categories = wp_get_post_categories($post_after->ID);

                    foreach ($post_categories as $catz) {
                        if (in_array($catz, $chosencat)) {
                            $dcatmatch = 1;
                            //break;
                        }

                    }
                    if ($dcatmatch == 1) {
                        foreach ($profile as $profs) {
                            foreach ($checkedlists as $checkedlist) {

                                if ($profs['id'] == $checkedlist['id']) {
                                    //$carr1=explode(',', $fbap_include_categories);


                                    $fb->post('/' . $profs['id'] . '/feed', ['message' => $post_after->post_title.'  final test  ' . get_the_author_meta('display_name', $post_after->post_author)], $profs['access_token']);
                                    //break;
                                }
                            }

                        }
                    }

                }

            }
        }
    }

}




add_action('post_updated','updatepost',10,3);

add_action('post_updated','wc_updatepost',10,3);
add_action( 'transition_post_status', 'get_latest_posts', 10, 3 );

