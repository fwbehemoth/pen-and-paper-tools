<?php
    require_once 'config.php';

    $lid = $_REQUEST['userid'];
    $name = $_REQUEST['name'];
    $type = $_REQUEST['type'];
    $race = $_REQUEST['race'];
    $age = $_REQUEST['age'];	
    $sex = $_REQUEST['sex'];
    $hght = $_REQUEST['height'];
    $wght = $_REQUEST['weight'];

    $query = "insert into Characters (userid, name, type, race, age, sex, height, weight, campaign) values ('".$lid."', '".$name."', '".$type."', '".$race."', '".$age."', '".$sex."', '".$hght."', '".$wght."', '".$camp."');";
    $result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error($database));
	
    $List['result'] = "created";
    $List['id'] = mysqli_insert_id($database);

    echo json_encode($List);
?>