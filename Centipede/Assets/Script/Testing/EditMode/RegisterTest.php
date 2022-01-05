<?php

include("config.php");
 
if(isset($_POST["newAccountUsername"])){
    $username = mysqli_real_escape_string($connection, $_POST["newAccountUsername"]);         

    // Check are they empty?
    if($username != ""){
        //Check is the username has not taken
        if(mysqli_num_rows(mysqli_query($connection, "SELECT * FROM unity_username WHERE username = '$username'")) == 0){           
            mysqli_query($connection, "INSERT INTO unity_username (user_id, username) VALUES (uuid(), '$username')");

            // Get user id response
            $sql = "SELECT user_id FROM unity_username WHERE username = '$username'";
            $result = mysqli_query($connection, $sql);
            $row = mysqli_fetch_row($result);
            $id = $row[0];          
                    
            echo "user_id: " . $id;

            $sql = "SELECT user_id FROM unity_username WHERE username = '$username'";
            $result = mysqli_query($connection, $sql);
            $row = mysqli_fetch_row($result);
            $id = $row[0]; 
            assertEquals($id);
        }else{
            echo "This Username is not available. Please use another username.";
        }       
    }else{
        echo "Both fields are required.";
    }    
}


?>