<?php
    require_once 'config.php';

    $mapid = $_REQUEST['id'];
    $query = "Select * FROM Maps WHERE Id ='". $mapid  ."';";
    $result=mysqli_query($database, $query) or die('Query failed: ' . mysqli_error($database));
    $item  = mysqli_fetch_array($result, MYSQLI_NUM);
    $uid =  $item['UserId'];
    $campid = $item['CampaignId'];
    
    $filename = "maps/".$uid."_".$campid."_".$mapid.".map";
    if(file_exists($filename)){
        $handle = fopen($filename, "r");
        $file = fread($handle, filesize($filename));
        $List['data'] = $file;
        $List['success'] = TRUE;
        fclose($handle);
    } else {
        $List['success'] = FALSE;
    }

    echo json_encode($List);
?>