import {
  Card,
  Descriptions,
  Table,
  Button,
} from 'antd';
import { GridContent, PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { FC, useEffect } from 'react';
import { connect, Dispatch, UserModelState, IUser, IPostClaimFormParam } from 'umi';
import styles from './index.less';
import { ClaimProcessModelState } from './model';

interface claimProcessProps {
  data?: IPostClaimFormParam;
  formid: string;
  currentUser?: IUser;
  dispatch: Dispatch;
}

const claimProcess: FC<claimProcessProps> = (props) => {
  const { dispatch, data, formid, currentUser } = props;
  useEffect(() => {
    dispatch({
      type: 'claimProcess/fetch',
      payload: {
        formid: formid
      }
    });
  }, [formid]);
  const columns = [
    {
      title: '化学品名称',
      dataIndex: 'name',
    },
    {
      title: '化学品数量',
      dataIndex: 'amount'
    },
    {
      title: '生产厂家',
      dataIndex: 'fname',
    }
  ]
  const dataSource = data?.chemicals.map(u => ({
    ...u,
    key: u.id
  }));
  return (
    <PageHeaderWrapper
      title="领用申请详情"
    >
      <div className={styles.main}>
        <GridContent>
          <Card style={{ marginBottom: 24 }} bordered={false} title={'表单'} >

            <Descriptions bordered>
              <Descriptions.Item label="申请编号">{data?.form.id}</Descriptions.Item>
              <Descriptions.Item label="所属实验室编号">{data?.form.lid}</Descriptions.Item>
              <Descriptions.Item label="提交时间">{data?.form.stime}</Descriptions.Item>
              <Descriptions.Item label="申请人姓名">{data?.form.uname}</Descriptions.Item>
              <Descriptions.Item label="预计归还时间">{data?.form.rtime}</Descriptions.Item>
              <Descriptions.Item label="当前状态">{data?.form.state}</Descriptions.Item>
            </Descriptions>
          </Card>
          <Card style={{ marginBottom: 24 }} bordered={false} title='申请的危险品'>
            <Table dataSource={dataSource} columns={columns}>
            </Table>
          </Card>
        </GridContent>
      </div>
      <div>
        <Button type="primary"
          style={{ width: '40%', marginRight: '3em', marginLeft: '10%' }}
          onClick={() => {
            dispatch({
              type: 'claimProcess/approve',
              payload: {
                uid: currentUser?.userId,
                uname: currentUser?.realName,
                lid: currentUser?.labId,
                fid: parseInt(formid)
              }
            })
          }}>
          同意
        </Button>
        <Button type="primary" style={{ width: '40%' }}
          onClick={() => {
            dispatch({
              type: 'claimProcess/reject',
              payload: {
                uid: currentUser?.userId,
                uname: currentUser?.realName,
                lid: currentUser?.labId,
                fid: parseInt(formid)
              }
            })
          }}>
          驳回
        </Button>
      </div>
    </PageHeaderWrapper>
  );
}

export default connect(
  ({
    claimProcess,
    user,
  }: {
    claimProcess: ClaimProcessModelState;
    user: UserModelState;
  }, ownProps: any) => ({
    currentUser: user.currentUser,
    formid: ownProps.match.params.formid,
    data: claimProcess.data,
  }),
)(claimProcess);
