import React, { Component, Fragment } from 'react';
import { Pagination } from '../SubComponents/Pagination';
import { AppApiPaths } from '../Api-authorization/AppConstants';
import { OrderCard } from '../SubComponents/OrderCard';

export class OrderList extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            orders: [],
            pageIsLast: false,
            dataWasUpdated: true,
            access: false,
        }
    }

    componentDidMount() {
        this.getOrdersFromServer();
    }

    componentDidUpdate() {
        if (!this.state.dataWasUpdated)
            this.getOrdersFromServer();
    }

    async getPermision() {
        let response = await fetch(AppApiPaths.Permision, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            console.log("error")
        }
        else {
            this.setState({ access: (await response.json()).access });
        }
    }

    async getOrdersFromServer() {
        //The server pagination starts from zero
        let response = await fetch(AppApiPaths.Orders + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
            console.log("error");
        else {
            const answer = await response.json();
            this.setState({ orders: answer.orders, pageIsLast: answer.pageIsLast, dataWasUpdated: true });
        }
    }

    render() {
        const orders = this.state.orders;
        if (orders == null)
            return (<div />);

        //need check it. If server return 0 orders, client will get null or empty array?
        if (orders.length === 0)
            return (
                <div className="row mt-3 ">
                    <div className="col">
                        <h3>У вас нет заказов</h3>
                    </div>
                </div>);

        let hiddenButtons = (
            <div>
                <div className="d-flex justify-content-center mt-3 ">
                    <div className="">
                        <button>Мои заказы</button>
                    </div>
                    <div className="">
                        <button>Все заказы</button>
                    </div>
                </div>
            </div>);

        return (
            <Fragment>
                {this.state.access ? }
                <div className="row no-gutters mt-3 ">
                    <div className="col">
                        <h3>Ваши заказы:</h3>
                    </div>
                </div>
                {orders.map((order, i) => <OrderCard key={i} order={order} />)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>);
    }
}