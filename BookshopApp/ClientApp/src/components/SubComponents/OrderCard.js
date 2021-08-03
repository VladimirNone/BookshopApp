import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { AppPagePaths } from '../Api-authorization/AppConstants';

export class OrderCard extends Component {

    render() {
        const order = this.props.order;

        return (
            <Link className="card mt-3 bg-light order_card" to={AppPagePaths.Order + "/" + order.id}>
                <div className="row no-gutters align-items-center ordered_card">
                    <div className="col-md">
                        <div className="card-body">
                            <h5 className="card-title">Номер: {order.id}</h5>
                            <p className="card-text">Дата оформления заказа: {new Date(order.dateOfOrdering).toLocaleString()}</p>
                            <p className="card-text">Дата закрытия заказа: {new Date(order.dateOfClosing).toLocaleString()}</p>
                            <p className="card-text">Стоимость:{order.finalAmount}</p>
                            <p className="card-text">Статус: {order.state.nameOfState}</p>
                            {(this.props.admin === true && order.customer != null) ? <p className="card-text">Заказчик: {order.customer.userName}</p> : null}
                        </div>
                    </div>
                </div>
            </Link>);
    }
}