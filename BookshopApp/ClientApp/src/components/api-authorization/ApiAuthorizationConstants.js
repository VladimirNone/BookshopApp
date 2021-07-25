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
const myprefix = '/Account'

export const ApplicationPaths = {
    DefaultLoginRedirectPath: '/',
    ApiAuthorizationClientConfigurationUrl: `_configuration/${ApplicationName}`,
    ApiAuthorizationPrefix: prefix,

    Login: `${myprefix}/Login`,

    LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
    LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,

    Register: `${myprefix}/Signup`,

    Profile: `${prefix}/${LoginActions.Profile}`,
    LogOut: `${prefix}/${LogoutActions.Logout}`,
    LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
    LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`,
    IdentityRegisterPath: 'Identity/Account/Register',
    IdentityManagePath: 'Identity/Account/Manage',

    IsUserAuthenticated: `${myprefix}/Check`

};
