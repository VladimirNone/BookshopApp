import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Logout } from './components/Logout';
import { Signup } from './components/Signup';
import { ApplicationPaths } from './components/Api-authorization/ApiConstants';

import './custom.css'
import { Content } from './components/Content';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Switch>
                    <Route path={ApplicationPaths.Login} component={Login} />
                    <Route path={ApplicationPaths.Logout} component={Logout} />
                    <Route path={ApplicationPaths.Register} component={Signup} />

                    <Route path="/" component={Content}/>
                </Switch>
            </Layout>
        );
    }
}
