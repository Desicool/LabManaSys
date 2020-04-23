import React, { FC } from 'react';
import { connect, FormToProcessState, history } from 'umi';
import { IClaimForm } from '@/models/entity';
import { Table } from 'antd';

interface ClaimProcessProps {
    claims: IClaimForm[];
    loading: boolean;
}
const FinancialProcessComponent: FC<ClaimProcessProps> = (props) => {
    const { claims } = props;
    const dataSource = claims.map(u => ({ ...u, key: u.id }));

    const columns = [
        {
            title: '编号',
            dataIndex: 'id',
            width: '6em'
        },
        {
            title: '申请人',
            dataIndex: 'uname',
            width: '12em'
        },
        {
            title: '所属实验室',
            dataIndex: 'lid',
            width: '10em'
        },
        {
            title: '预计归还时间',
            dataIndex: 'rtime'
        },
        {
            title: '进行处理',
            key: 'operation',
            render: (_: any, record: IClaimForm) => <a onClick={() => history.push('/process/claim/' + record.id)}>查看详情</a>,
        },
    ];
    return <div><Table dataSource={dataSource} columns={columns} bordered={true} />
    </div>
}

export default connect(
    ({
        myFormToProcess,
        loading,
    }: {
        myFormToProcess: FormToProcessState;
        loading: {
            effects: { [key: string]: boolean };
        };
    }) => ({
        claims: myFormToProcess.msg?.cform || [],
        loading: loading.effects['myFormToProcess/fetchAdvanced'],
    }),
)(FinancialProcessComponent);
