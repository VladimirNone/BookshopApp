import React, { Component, Fragment } from 'react';
import { Link } from 'react-router-dom';
import { ApplicationPaths } from './api-authorization/ApiAuthorizationConstants';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props){
        super(props);
        this.state = { login: null, password: null, success: false };
    }

    render() {
        return (
            <Fragment>
                <h1>Log in</h1>
                <div className="row">
                    <div className="col-md-4">
                        <section>
                            <form id="account" method="post" target="/some">
                                <h2>Use a local account to log in.</h2>
                                <hr />
                                <div className="text-danger"></div>
                                <div className="form-group">
                                    <label>Email</label>
                                    <input name="Login" className="form-control"/>
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <label>Password</label>
                                    <input name="Password" className="form-control" />
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <button id="login-submit" type="submit" className="btn btn-primary">Log in</button>
                                </div>
                                <div className="form-group">
                                    <p>
                                        <Link to={ApplicationPaths.Register}>Register as a new user</Link>
                                    </p>
                                </div>
                            </form>
                        </section>
                    </div>
                </div>
            </Fragment>
        );
    }
}


