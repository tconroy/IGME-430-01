<?php
session_start();

if( ! isset( $_SESSION['tacos'] ) ){
  header("Location: Start.php");
  die();
}
else{
  require("PageBuilder.php");
}

echo addHTMLHeader();
$time = time();
$diff = $time - $_SESSION['last_access'];
$_SESSION['last_access'] = $time;
$_SESSION['tacos'] += $diff;

if( $_SERVER['REQUEST_METHOD'] === 'POST' ) {
  $_SESSION['tacos_eaten'] += $_SESSION['tacos'];
  $_SESSION['tacos'] = 1;
}

$opts="";
$colorChoices = ['default', 'green', 'red', 'blue', 'orange'];
foreach ($colorChoices as $color) {
  $selected='';
  if( isset($_COOKIE['bgColor']) && $color == $_COOKIE['bgColor'] ){
    $selected = "selected='selected'";
  }
  $opts .= "<option {$selected} value='{$color}'>$color</option>";
}

echo <<<HTML
<div id="profile">
  <p>Welcome {$_COOKIE['user']}</p>
  <a href="QuitGame.php">Quit Game</a>
  <select name="color" id="color">{$opts}</select>
</div>
<div id="tacos">
  <a href="Game.php">Refresh</a>
  <p>Time Passed: <span id="timer">1</span></p>
  <p>You have {$_SESSION['tacos']} taco(s).</p>
  <p>You have eaten {$_SESSION['tacos_eaten']} tacos.</p>
  <form name="eat" action="Game.php" method="POST" accept-charset="utf-8">
    <input type="submit" value="Eat Tacos">
  </form>
</div>
HTML;
echo addHTMLFooter();
?>