<?php
    require_once 'config.php';
    $data = $GLOBALS["HTTP_RAW_POST_DATA"];
    $json = json_decode($data, TRUE);
    $map = json_encode($json['map']);
    $mapid = $json['id'];
    
    $query = "Select * FROM Maps WHERE Id ='". $mapid  ."';";
    $result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error($database));
    $item = mysqli_fetch_array($result);
    $uid = $item['UserId'];
    $campid = $item['CampaignId'];
    
    $file = fopen("maps/".$uid."_".$campid."_".$mapid.".map", 'w');
    fwrite($file, $map);
    fclose($file);
    $response['mapid'] = $mapid;
    $response['uid'] = $uid;
    $response['campid'] = $campid;
    $response['success'] = 'success';
    
    echo json_encode($response);
?>