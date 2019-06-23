<?php
    require_once 'config.php';

    $lid = $_REQUEST['userid'];
    $cid = $_REQUEST['campaignid'];

    $qchar = "SELECT * FROM Maps WHERE UserId = '".$lid."' AND CampaignId = '".$cid."';";
    $charresult = mysqli_query($database, $qchar) or die('Query failed: ' . mysqli_error($database));

    class Item {
            var $Id;
            var $Name;
    }

    //$arrchar = mysql_fetch_array($charresult);

    $cnt = 0;
    while($item = mysqli_fetch_array($charresult, MYSQLI_BOTH)){
            $List[$cnt] = new Item();
            $List[$cnt] -> Id = $item['Id'];
            $List[$cnt] -> Name = $item['Name'];
            $cnt++;
    }

    $List['count'] = $cnt;
    echo json_encode($List);
?>