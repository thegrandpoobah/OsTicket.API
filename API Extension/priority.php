<?php
file_exists('../main.inc.php') or die('System Error');

// Disable sessions for the API. API should be considered stateless and
// shouldn't chew up database records to store sessions
if (!function_exists('noop')) { function noop() {} }
session_set_save_handler('noop','noop','noop','noop','noop','noop');
define('DISABLE_SESSION', true);

require_once('../main.inc.php');
require_once(INCLUDE_DIR.'class.api.php');
require_once(INCLUDE_DIR.'class.priority.php');

$controller = new ApiController();
$controller->requireApiKey();

$x = new Priority();
echo json_encode($x->getPriorities(true));
?>