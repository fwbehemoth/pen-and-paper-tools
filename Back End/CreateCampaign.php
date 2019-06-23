<?php
    require_once 'config.php';

    $uid = $_REQUEST['userid'];
    $name = $_REQUEST['name'];
    $game = $_REQUEST['game'];
    $set = $_REQUEST['set'];
    $add = $_REQUEST['add'];

    $query = "insert into Campaigns (userid, name, game, setting, address) values ('".$uid."', '".$name."', '".$game."', '".$set."', '".$add."');";
    $result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error($database));

    $List['result'] = "created";
    $List['id'] = mysqli_insert_id($database);

    echo json_encode($List);
?>