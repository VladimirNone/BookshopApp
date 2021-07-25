import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Signup } from './components/Signup';
import { MainPage } from './components/MainPage';
/*import { Product } from './components/Product';
import { Basket } from './components/Basket';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';*/
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={MainPage} />
                <Route path={ApplicationPaths.Login} component={Login} />
                <Route path={ApplicationPaths.Register} component={Signup} />
                {/*        <Route path='/product' component={Product} />
                    <Route path='/counter' component={Counter} />
                    <Route path='/basket' component={Basket} />*/}
                {/*<AuthorizeRoute path='/fetch-data' component={FetchData} />*/}
                {/*<Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />*/}
            </Layout>
        );
    }
}
