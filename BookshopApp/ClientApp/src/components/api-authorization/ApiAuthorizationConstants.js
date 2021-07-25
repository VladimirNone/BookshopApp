export const ApplicationName = 'BookshopApp';

export const QueryParameterNames = {
    ReturnUrl: 'returnUrl',
    Message: 'message'
};

export const LogoutActions = {
    LogoutCallback: 'logout-callback',
    Logout: 'logout',
    LoggedOut: 'logged-out'
};

export const LoginActions = {
    Login: 'login',
    LoginCallback: 'login-callback',
    LoginFailed: 'login-failed',
    Profile: 'profile',
    Register: 'register'
};

const prefix = '/authentication';
const accountPrefix = '/Account'

export const ApplicationPaths = {
    DefaultLoginRedirectPath: '/',
    ApiAuthorizationClientConfigurationUrl: `_configuration/${ApplicationName}`,
    ApiAuthorizationPrefix: prefix,

    LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
    LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,

    Login: `${accountPrefix}/Login`,
    Register: `${accountPrefix}/Signup`,
    Profile: `${accountPrefix}/Profile`,
    LogOut: `${accountPrefix}/Logout`,

    LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
    LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`,
    IdentityRegisterPath: 'Identity/Account/Register',
    IdentityManagePath: 'Identity/Account/Manage',

    IsUserAuthenticated: `${accountPrefix}/Check`

};
